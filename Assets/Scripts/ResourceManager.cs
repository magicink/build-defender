using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public delegate void OnAvailableResourcesChanged(Dictionary<ResourceData, int> resourceTypes);

    public OnAvailableResourcesChanged HandleAvailableChange;

    private readonly Dictionary<ResourceData, int> _available = new Dictionary<ResourceData, int>();
    private readonly Dictionary<ResourceData, int> _accumulated = new Dictionary<ResourceData, int>();
    private ResourceCollection _source;
    private float _elapsed;

    public Dictionary<ResourceData, int> Available => _available;

    private void Awake()
    {
        Instance = this;
        _source = Resources.Load<ResourceCollection>(nameof(ResourceCollection));
        if (!_source) return;
        if (_source.data.Count <= 0) return;
        foreach (var resourceType in _source.data)
        {
            _available.Add(resourceType, 0);
            _accumulated.Add(resourceType, 0);
        }
    }

    private void Update()
    {
        TransferAccumulatedResources();
    }

    private void Start()
    {
        BuildingManager.Instance.HandleBuildingConstructed += UpdateAvailableResources;
    }

    private void UpdateAvailableResources(BuildingData buildingType)
    {
        var costs = buildingType.constructionCosts.data;
        foreach (var cost in costs)
        {
            _available[cost.resourceType] -= cost.amount;
            HandleAvailableChange?.Invoke(_available);
        }
    }


    public void AddAccumulationListener (ResourceGenerator generator)
    {
        generator.HandleResourceGeneration += AccumulateResource;
    }

    public void RemoveAccumulationListener(ResourceGenerator generator)
    {
        generator.HandleResourceGeneration -= AccumulateResource;
    }

    private void AccumulateResource(ResourceData resourceType, ResourceGenerator generator)
    {
        _accumulated[resourceType] = Mathf.Clamp(_accumulated[resourceType] + generator.TotalNodes, 0, 999);
    }

    private void TransferAccumulatedResources()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed < 1.5f) return;
        _elapsed = 0;
        if (!_source) return;
        foreach (var resourceType in _source.data)
        {
            _available[resourceType] = Mathf.Clamp(_accumulated[resourceType] + _available[resourceType], 0, 999); 
            _accumulated[resourceType] = 0;
        }
        HandleAvailableChange?.Invoke(_available);
    }
}

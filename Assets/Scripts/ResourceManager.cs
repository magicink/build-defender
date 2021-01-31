using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public delegate void OnResourceChanged(Dictionary<ResourceType, int> resourceTypes);

    public OnResourceChanged HandleResourceChanged;

    private readonly Dictionary<ResourceType, int> _available = new Dictionary<ResourceType, int>();
    private readonly Dictionary<ResourceType, int> _accumulated = new Dictionary<ResourceType, int>();
    private ResourceTypes _source;
    private float _elapsed;

    public Dictionary<ResourceType, int> Available => _available;

    private void Awake()
    {
        Instance = this;
        _source = Resources.Load<ResourceTypes>(nameof(ResourceTypes));
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
        _elapsed += Time.deltaTime;
        if (_elapsed < 1.5f) return;
        _elapsed = 0;
        if (!_source) return;
        foreach (var resourceType in _source.data)
        {
            _available[resourceType] = Mathf.Clamp(_accumulated[resourceType] + _available[resourceType], 0, 999); 
            _accumulated[resourceType] = 0;
        }
        HandleResourceChanged?.Invoke(_available);
    }


    public void AddListener (ResourceGenerator generator)
    {
        generator.HandleResourceGeneration += AddResource;
    }

    public void RemoveListener(ResourceGenerator generator)
    {
        generator.HandleResourceGeneration -= AddResource;
    }

    private void AddResource(ResourceType resourceType, ResourceGenerator generator)
    {
        _accumulated[resourceType] = Mathf.Clamp(_accumulated[resourceType] + generator.TotalNodes, 0, 999);
    }
}

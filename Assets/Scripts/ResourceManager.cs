using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public delegate void OnResourceChanged(Dictionary<ResourceType, int> resourceTypes);

    public OnResourceChanged HandleResourceChanged;

    private readonly Dictionary<ResourceType, int> _resourceTypes = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        Instance = this;
        var source = Resources.Load<ResourceTypes>(nameof(ResourceTypes));
        if (!source) return;
        if (source.data.Count <= 0) return;
        foreach (var resourceType in source.data)
        {
            _resourceTypes.Add(resourceType, 0);
        }
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
        _resourceTypes[resourceType] = Mathf.Clamp(_resourceTypes[resourceType] + 1, 0, 999);
        HandleResourceChanged?.Invoke(_resourceTypes);
    }
}

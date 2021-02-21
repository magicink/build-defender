using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeter : MonoBehaviour
{
    [SerializeField] private ResourceData resourceType;
    [SerializeField] private Image icon;
    [SerializeField] private int count;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI displayShadow;

    private delegate void OnResourceTypeAdded();

    private OnResourceTypeAdded _handleResourceTypeAdded;

    public ResourceData ResourceType
    {
        set
        {
            resourceType = value;
            _handleResourceTypeAdded?.Invoke();
        }
    }

    private void Awake()
    {
        _handleResourceTypeAdded += HandleResourceTypeAdded;
    }

    private void Start()
    {
        ResourceManager.Instance.HandleAvailableChange += HandleResourcesUpdate;
    }

    private void Update()
    {
        displayText.text = $"{count}";
        displayShadow.text = $"{count}";
    }

    private void HandleResourceTypeAdded()
    {
        if (!resourceType) return;
        if (icon) icon.sprite = resourceType.icon;
    }

    private void HandleResourcesUpdate(IReadOnlyDictionary<ResourceData, int> types)
    {
        if (resourceType) count = types[resourceType];
    }

    private void OnDestroy()
    {
        _handleResourceTypeAdded -= HandleResourceTypeAdded;
        ResourceManager.Instance.HandleAvailableChange -= HandleResourcesUpdate;
    }
}

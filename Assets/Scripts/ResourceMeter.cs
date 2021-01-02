using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMeter : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private Image icon;
    [SerializeField] private int count;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI displayShadow;

    private delegate void OnResourceChangeChange();

    private OnResourceChangeChange _handleResourceChange;

    public ResourceType ResourceType
    {
        set
        {
            resourceType = value;
            _handleResourceChange?.Invoke();
        }
    }

    private void Awake()
    {
        _handleResourceChange += HandleResourceTypeAdded;
    }

    private void Start()
    {
        ResourceManager.Instance.HandleResourceChanged += HandleResourcesUpdate;
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

    private void HandleResourcesUpdate(IReadOnlyDictionary<ResourceType, int> types)
    {
        if (resourceType) count = types[resourceType];
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.HandleResourceChanged -= HandleResourcesUpdate;
    }
}

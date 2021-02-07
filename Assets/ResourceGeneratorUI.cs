using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceLabel;

    private ResourceGenerator _resourceGenerator;

    private void Start()
    {
        if (!buildingData || !resourceIcon) return;
        _resourceGenerator = buildingData.GetComponent<ResourceGenerator>();
        resourceIcon.sprite = buildingData.BuildingType.resourceGeneratorData.ResourceType.icon;
    }

    private void Update()
    {
        if (_resourceGenerator && resourceLabel)
        {
            resourceLabel.text = $"{_resourceGenerator.TotalNodes} / {buildingData.BuildingType.maxNodes}";
        }
    }
}

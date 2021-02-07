using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private Image resourceIcon;

    private void Start()
    {
        if (buildingData && resourceIcon)
        {
            resourceIcon.sprite = buildingData.BuildingType.resourceGeneratorData.ResourceType.icon;
        }
    }
}

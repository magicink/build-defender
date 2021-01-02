using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingSelection : MonoBehaviour
{
    [SerializeField] private BuildingUIData buildingUIData;
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private Image icon;
    [SerializeField] private Image background;


    public BuildingType BuildingType
    {
        set
        {
            buildingType = value;
            _handleBuildingTypeAdded?.Invoke();
        }
    }

    private delegate void OnBuildingTypeAdded();

    private OnBuildingTypeAdded _handleBuildingTypeAdded;

    private void Awake()
    {
        _handleBuildingTypeAdded += SetIconSprite;
        var button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);

        if (background && buildingUIData)
        {
            background.color = buildingUIData.color;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.HandleCurrentBuildingChanged += HandleCurrentBuildingChanged;
    }

    private void SetIconSprite()
    {
        if (icon && buildingType)
        {
            icon.sprite = buildingType.icon;
        }
    }

    private void HandleClick()
    {
        BuildingManager.Instance.CurrentBuilding = buildingType;
    }

    private void HandleCurrentBuildingChanged(BuildingType currentBuildingType)
    {
        background.color = buildingType == currentBuildingType ? buildingUIData.selectedColor : buildingUIData.color;
    }
}
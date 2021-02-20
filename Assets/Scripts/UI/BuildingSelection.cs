using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildingSelection : MonoBehaviour
{
    [SerializeField] private BuildingUIData buildingUIData;
    [SerializeField] private BuildingData buildingType;
    [SerializeField] private Image icon;
    [SerializeField] private Image background;

    private Button _button;
    private BuildingData _selectedBuildingType;
    public Image Icon { get; }

    public BuildingData BuildingType
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
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);

        if (background && buildingUIData)
        {
            background.color = buildingUIData.color;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.HandleCurrentBuildingChanged += HandleCurrentBuildingChanged;
        if (icon) icon.material = buildingType.iconMaterial;
    }

    private void Update()
    {
        if (!_button) return;
        var selected = buildingType == _selectedBuildingType;
        var buttonEnabled = buildingType != _selectedBuildingType && BuildingManager.CanAfford(buildingType);
        background.color = selected ? buildingUIData.selectedColor : buttonEnabled ? buildingUIData.color : buildingUIData.disabledColor;
        _button.enabled = buttonEnabled;
        if (icon.material.HasProperty(MaterialProperties.Alpha))
        {
            icon.material.SetFloat(MaterialProperties.Alpha, buttonEnabled || selected ? 1.0f : 0.5f);
        }
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

    private void HandleCurrentBuildingChanged([CanBeNull] BuildingData currentBuildingType)
    {
        if (!_button) return;
        _selectedBuildingType = currentBuildingType;
    }
}
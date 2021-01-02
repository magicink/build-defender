using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    private Camera _cameraMain;
    private BuildingType _currentBuilding;

    public BuildingType CurrentBuilding
    {
        set
        {
            if (_currentBuilding == value) return;
            _currentBuilding = value;
            HandleCurrentBuildingChanged?.Invoke(_currentBuilding);
        }
    }

    public delegate void OnCurrentBuildingChanged(BuildingType buildingType);

    public OnCurrentBuildingChanged HandleCurrentBuildingChanged;

    private void Awake()
    {
        Instance = this;
        _cameraMain = Camera.main;
    }

    private void Update()
    {
        if (!_currentBuilding || !Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
        var buildingPrefab = _currentBuilding.prefab;
        if (buildingPrefab)
        {
            Instantiate(buildingPrefab, Utils.GetMousePosition(), Quaternion.identity);
        }
    }
}

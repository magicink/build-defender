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
        // _cameraMain = Camera.main;
    }

    private void Update()
    {
        var mousePosition = Utils.GetMousePosition();
        if (!_currentBuilding || !Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
        if (!CanSpawn(_currentBuilding, mousePosition)) return;
        var buildingPrefab = _currentBuilding.prefab;
        if (buildingPrefab)
        {
            Instantiate(buildingPrefab, mousePosition, Quaternion.identity);
        }
    }

    private static bool CanSpawn(BuildingType buildingType, Vector3 position)
    {
        var collider2d = buildingType.prefab.GetComponent<BoxCollider2D>();
        if (!collider2d) return false;
        var collisions = Physics2D.OverlapBoxAll(position + (Vector3) collider2d.offset, collider2d.size, 0);
        // 2 Because the ghost building has a collider
        return collisions.Length < 2;
    }
}

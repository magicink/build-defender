using System.Linq;
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
        if (!CanAfford(buildingType)) return false;
        var collisions = Physics2D.OverlapBoxAll(position + (Vector3) collider2d.offset, collider2d.size, 0);
        if (collisions.Length > 1) return false;
        var possibleConflicts =
            Physics2D.OverlapCircleAll(position + (Vector3) collider2d.offset, buildingType.range);
        if (possibleConflicts.Select(collision => collision.GetComponent<BuildingData>()).Where(buildingData => buildingData).Any(buildingData => buildingData.BuildingType == buildingType))
        {
            return false;
        }
        var nearbyBuildings = Physics2D.OverlapCircleAll(position + (Vector3) collider2d.offset, buildingType.range * 1.5f);
        return nearbyBuildings.Select(nearbyBuilding => nearbyBuilding.GetComponent<BuildingData>()).Any(buildingData => buildingData);
    }

    private static bool CanAfford(BuildingType buildingType)
    {
        var costs = buildingType.constructionCosts;
        return costs.data.All(data => data.cost <= ResourceManager.Instance.Available[data.resourceType]);
    }
}

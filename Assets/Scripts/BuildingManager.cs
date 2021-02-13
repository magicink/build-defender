using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

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

    public HealthController Headquarters { get; private set; }

    public delegate void OnCurrentBuildingChanged([CanBeNull] BuildingType buildingType);

    public delegate void OnBuildingConstructed(BuildingType buildingType);

    public OnCurrentBuildingChanged HandleCurrentBuildingChanged;
    public OnBuildingConstructed HandleBuildingConstructed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!Instance.Headquarters) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentBuilding)
            {
                CurrentBuilding = null;
            }

            return;
        }
        var mousePosition = Utils.GetMousePosition();
        if (!_currentBuilding || !Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;
        if (!CanSpawn(_currentBuilding, mousePosition)) return;
        var buildingPrefab = _currentBuilding.prefab;
        if (buildingPrefab)
        {
            HandleBuildingConstructed?.Invoke(_currentBuilding);
            Instantiate(buildingPrefab, mousePosition, Quaternion.identity);
            CurrentBuilding = null;
        }
    }

    private static bool CanSpawn(BuildingType buildingType, Vector3 position)
    {
        if (!Instance.Headquarters) return false;
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

    public static bool CanAfford(BuildingType buildingType)
    {
        if (!Instance.Headquarters) return false;
        var costs = buildingType.constructionCosts;
        return costs.data.All(data => data.amount <= ResourceManager.Instance.Available[data.resourceType]);
    }

    public void SetHeadquarters(GameObject target)
    {
        if (Headquarters) return;
        var headquartersHealthController = target.GetComponent<HealthController>();
        if (headquartersHealthController)
        {
            Headquarters = headquartersHealthController;
            Headquarters.handleHeadquartersDestroyed += HandleHeadquartersDestroyed;
        }
    }

    private void HandleHeadquartersDestroyed()
    {
        Headquarters.handleHeadquartersDestroyed -= HandleHeadquartersDestroyed;
        Headquarters = null;
    }
}

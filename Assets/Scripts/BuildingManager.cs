using UnityEngine;

public class BuildingManager : MonoBehaviour
{


    private Camera _cameraMain;
    private BuildingTypes _buildingTypes;
    private BuildingType _currentBuilding;
    private int _currentIndex = 0;

    private void Awake()
    {
        _cameraMain = Camera.main;
        _buildingTypes = Resources.Load<BuildingTypes>("BuildingTypes");
        if (!_buildingTypes || _buildingTypes.data.Count <= 0) return;
        _currentBuilding = _buildingTypes.data[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_buildingTypes.data.Count > 0)
            {
                var nextIndex = _currentIndex + 1;
                if (nextIndex + 1 > _buildingTypes.data.Count)
                {
                    nextIndex = 0;
                }
                _currentBuilding = _buildingTypes.data[nextIndex];
                _currentIndex = nextIndex;
            }
        }
        if (Input.GetMouseButtonDown(0) && _currentBuilding)
        {
            var buildingPrefab = _currentBuilding.prefab;
            if (buildingPrefab)
            {
                Instantiate(buildingPrefab, GetScreenPointToWorld(), Quaternion.identity);
            }
        }

    }

    private Vector3 GetScreenPointToWorld()
    {
        var mousePosition = _cameraMain.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        return mousePosition;
    }
}

using UnityEngine;

public class BuildingDataController : MonoBehaviour, IHealthDataProvider
{
    [SerializeField] private BuildingData buildingType;
    [SerializeField] private bool isHeadquarters;

    public BuildingData BuildingType => buildingType;
    public IHealthData HealthData => buildingType;

    private void Start()
    {
        if (isHeadquarters)
        {
            BuildingManager.Instance.SetHeadquarters(gameObject);
        }
    }
}
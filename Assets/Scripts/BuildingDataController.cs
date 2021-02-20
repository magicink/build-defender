using UnityEngine;

public class BuildingDataController : MonoBehaviour, IHealthDataProvider
{
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private bool isHeadquarters;

    public BuildingType BuildingType => buildingType;
    public IHealthData HealthData => buildingType;

    private void Start()
    {
        if (isHeadquarters)
        {
            BuildingManager.Instance.SetHeadquarters(gameObject);
        }
    }
}
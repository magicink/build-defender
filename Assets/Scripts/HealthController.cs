using UnityEngine;

[RequireComponent(typeof(BuildingData))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int maxHitPoints;
    private BuildingData _buildingData;
    private BuildingType _buildingType;


    public int CurrentHitPoints
    {
        get => currentHitPoints;
        set => currentHitPoints = value;
    }

    public int MaxHitPoints
    {
        get => maxHitPoints;
        set => maxHitPoints = value;
    }

    private void Awake()
    {
        _buildingData = GetComponent<BuildingData>();
        _buildingType = _buildingData.BuildingType;
        if (_buildingData)
        {
            CurrentHitPoints = MaxHitPoints = _buildingType.startingHitPoints;
        }

    }
}

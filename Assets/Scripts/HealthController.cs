using UnityEngine;

public class HealthController : MonoBehaviour, IHealthController
{
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int maxHitPoints;

    public delegate void OnHeadquartersDestroyed();

    public OnHeadquartersDestroyed handleHeadquartersDestroyed;

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
        var buildingData = GetComponent<BuildingData>();
        if (buildingData)
        {
            var buildingType = buildingData.BuildingType;
            CurrentHitPoints = MaxHitPoints = buildingType.startingHitPoints;
            return;
        }

        var enemyData = GetComponent<EnemyDataController>();
        if (enemyData)
        {
            CurrentHitPoints = MaxHitPoints = enemyData.Data.hitPoints;
        }
    }

    private void Update()
    {
        if (currentHitPoints > 0) return;
        handleHeadquartersDestroyed?.Invoke();
        Destroy(gameObject);
    }
}

public interface IHealthController
{
    int CurrentHitPoints { get; set; }
    int MaxHitPoints { get; set; }
}

using UnityEngine;

public class HealthController : MonoBehaviour, IHealthController
{
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int maxHitPoints;

    public delegate void OnHeadquartersDestroyed();

    public OnHeadquartersDestroyed HandleHeadquartersDestroyed;

    public int CurrentHitPoints
    {
        get => currentHitPoints;
        set
        {
            if (value <currentHitPoints)
            {
                var building = gameObject.GetComponent<BuildingDataController>();
                if (building) CameraShake.Instance.StartShake(5.0f);
            }
            currentHitPoints = value;
        }
    }

    public int MaxHitPoints
    {
        get => maxHitPoints;
        set => maxHitPoints = value;
    }

    private void Awake()
    {
        var healthProvider = GetComponent<IHealthDataProvider>();
        if (healthProvider != null)
        {
            CurrentHitPoints = MaxHitPoints = healthProvider.HealthData.HitPoints;
        }
    }

    private void Update()
    {
        if (currentHitPoints > 0) return;
        var building = gameObject.GetComponent<BuildingDataController>();
        if (building) CameraShake.Instance.StartShake(15.0f);
        HandleHeadquartersDestroyed?.Invoke();
        Destroy(gameObject);
    }
}

public interface IHealthController
{
    int CurrentHitPoints { get; set; }
    int MaxHitPoints { get; set; }
}

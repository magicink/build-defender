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
            OnHealthChange(value, currentHitPoints);
            currentHitPoints = value;
        }
    }

    public int MaxHitPoints
    {
        get => maxHitPoints;
        set => maxHitPoints = value;
    }

    public virtual void OnHealthChange(int next, int current)
    {
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
        HandleHeadquartersDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
public interface IHealthController
{
    int CurrentHitPoints { get; set; }
    int MaxHitPoints { get; set; }

    void OnHealthChange(int next, int current);
}
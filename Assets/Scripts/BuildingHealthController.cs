public class BuildingHealthController : HealthController
{
    public override void OnHealthChange(int next, int current)
    {
        if (next >= current) return;
        var intensity = next <= 0 ? 5f : 2f;
    }
}
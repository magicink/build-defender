public class BuildingHealthController : HealthController
{
    public override void OnHealthChange(int next, int current)
    {
        if (next >= current) return;
        var intensity = next <= 0 ? 10f : 5f;
        CameraShake.Instance.StartShake(intensity);
    }
}
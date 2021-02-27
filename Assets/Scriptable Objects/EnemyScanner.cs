using UnityEngine;

public class EnemyScanner : TargetScanner
{
    private void Update()
    {
        if (!BuildingManager.Instance.Headquarters) return;
        LastScanTime -= Time.deltaTime;
        if (LastScanTime <= 0)
        {
            ScanTarget();
            LastScanTime = ScanFrequency;
        }
    }

    public override void ScanTarget()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, Range, LayerMask);
        if (results.Length > 0)
        {
            HandleTargetLocated?.Invoke(results[0].gameObject);
        }
    }
}
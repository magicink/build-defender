using UnityEngine;

public abstract class TargetScanner : MonoBehaviour, ITargetScanner
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;

    private float _lastScanTime;
    private float _scanFrequency;

    public delegate void OnTargetLocated(GameObject target);
    public OnTargetLocated HandleTargetLocated;

    public float Range
    {
        get => range;
        set => range = value;
    }

    public LayerMask LayerMask => layerMask;

    public float LastScanTime
    {
        get => _lastScanTime;
        set => _lastScanTime = value;
    }

    public float ScanFrequency => _scanFrequency;

    private void Awake()
    {
        _scanFrequency = Random.Range(0.1f, 0.5f);
    }

    public abstract void ScanTarget();
}
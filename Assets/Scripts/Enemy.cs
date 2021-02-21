using UnityEngine;

[RequireComponent(typeof(EnemyDataController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private EnemyDataController _enemyDataController;
    private Transform _destination;
    private Rigidbody2D _rigidbody2D;
    private float currentSpeed;
    private float _lastScanTime;
    private float _scanFrequency;

    private void Awake()
    {
        _enemyDataController = GetComponent<EnemyDataController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _scanFrequency = Random.Range(0.25f, 2.0f);
    }

    private void Start()
    {
        if (_enemyDataController)
        {
            if (spriteRenderer)
            {
                spriteRenderer.sprite = _enemyDataController.Data.sprite;
                spriteRenderer.color = _enemyDataController.Data.spriteColor;
            }

            currentSpeed = _enemyDataController.Data.speed;
        }
    }

    private void Update()
    {
        if (!_destination)
        {
            if (BuildingManager.Instance.Headquarters)
            {
                _destination = BuildingManager.Instance.Headquarters.transform;
            }
            else
            {
                _destination = transform;
            }
        }
        if (_destination && _rigidbody2D)
        {
            var dir = (_destination.position - transform.position).normalized;
            _rigidbody2D.velocity = dir * currentSpeed;
        }

        _lastScanTime -= Time.deltaTime;
        if (!(_lastScanTime <= 0f)) return;
        _lastScanTime = _scanFrequency;
        ScanTargets();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var building = other.gameObject.GetComponent<HealthController>();
        if (!building) return;
        building.CurrentHitPoints -= _enemyDataController.Data.damage;
        Destroy(gameObject);
    }

    private void ScanTargets()
    {
        var collisions = Physics2D.OverlapCircleAll(transform.position, _enemyDataController.Data.scanRadius);
        foreach (var collision in collisions)
        {
            var building = collision.gameObject.GetComponent<BuildingDataController>();
            if (!building) continue;
            if (!_destination)
            {
                _destination = building.transform;
                return;
            }
            if (building.transform != _destination)
            {
                _destination = GetCloser(_destination, building.transform);
            }
        }
    }

    private Transform GetCloser(Transform a, Transform b)
    {
        var position = transform.position;
        return Vector3.Distance(position, a.position) < Vector3.Distance(position, b.position) ? a : b;
    }
}
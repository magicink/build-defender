using UnityEngine;

[RequireComponent(typeof(EnemyDataController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private EnemyDataController _enemyDataController;
    private Transform _destination;
    private Rigidbody2D _rigidbody2D;
    private float currentSpeed;

    private void Awake()
    {
        _enemyDataController = GetComponent<EnemyDataController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var building = other.gameObject.GetComponent<HealthController>();
        if (!building) return;
        building.CurrentHitPoints -= _enemyDataController.Data.damage;
        Destroy(gameObject);
    }
}

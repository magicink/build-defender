using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData data;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Vector3 _destination;
    private float lifespan;
    private bool destinationIsSet;
    private bool angleIsSet;
    private Vector3 startPosition;
    private Vector3 direction;

    public LayerMask TargetLayer { get; set; }

    public ProjectileData Data => data;

    public Vector3 Destination
    {
        get => _destination;
        set
        {
            if (destinationIsSet) return;
            direction = (value - transform.position).normalized;
            transform.eulerAngles = new Vector3(0, 0, Utils.GetAngle(direction));
            _destination = value;
            destinationIsSet = true;
        }
    }

    private void Awake()
    {
        if (data && spriteRenderer)
        {
            spriteRenderer.sprite = data.sprite;
        }
    }

    private void Start()
    {
        startPosition = transform.position;
    }


    // Update is called once per frame
    private void Update()
    {
        if (!destinationIsSet) return;
        if (!angleIsSet)
        {
            transform.eulerAngles = new Vector3(0, 0, Utils.GetAngle(direction));
            angleIsSet = true;
        }

        var forceDestroy = false;
        var position = transform.position;
        if (Vector2.Distance(startPosition, position) < data.range)
        {
            transform.position += direction * (data.speed * Time.deltaTime);
        }
        else
        {
            forceDestroy = true;
        }
        lifespan += Time.deltaTime;
        if (lifespan >= data.timeToLive || forceDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var healthController = other.GetComponent<HealthController>();
        if (!healthController) return;
        if (TargetLayer != (TargetLayer | 1 << healthController.gameObject.layer)) return;
        healthController.CurrentHitPoints -= data.damage;
        Destroy(gameObject);
    }
}

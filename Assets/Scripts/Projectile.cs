using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData data;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Vector3 _destination;
    private float lifespan;
    private bool destinationSet;
    private LayerMask targetLayerMask;

    public LayerMask TargetLayerMask
    {
        get => targetLayerMask;
        set => targetLayerMask = value;
    }

    public ProjectileData Data => data;

    public Vector3 Destination
    {
        get => _destination;
        set
        {
            if (!destinationSet)
            {
                _destination = value;
                destinationSet = true;
            }
        }
    }

    private void Awake()
    {
        if (data && spriteRenderer)
        {
            spriteRenderer.sprite = data.sprite;
        }

        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (!destinationSet) return;
        var position = transform.position;
        if (Vector2.Distance(_destination, position) > 0)
        {
            transform.position = Vector3.MoveTowards(position, _destination, data.speed * Time.deltaTime);
        }
        lifespan += Time.deltaTime;
        if (lifespan >= data.timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var healthController = other.GetComponent<HealthController>();
        if (!healthController) return;
        if (TargetLayerMask != (TargetLayerMask | 1 << healthController.gameObject.layer)) return;
        healthController.CurrentHitPoints -= data.damage;
        Destroy(gameObject);
    }
}

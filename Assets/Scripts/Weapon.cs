using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private TargetScanner scanner;

    private float _cooldown;

    // Start is called before the first frame update
    private void Start()
    {
        if (!scanner || !data) return;
        scanner.Range = data.Projectile.Data.range;
        scanner.HandleTargetLocated += Attack;
    }

    private void Update()
    {
        if (!(_cooldown > 0)) return;
        _cooldown -= Time.deltaTime;
    }

    private void Attack(GameObject target)
    {
        if (_cooldown <= 0)
        {
            var projectile = Instantiate(data.Projectile, transform.position, Quaternion.identity);
            projectile.TargetLayer = scanner.LayerMask;
            projectile.Destination = target.gameObject.transform.position;
            _cooldown = data.fireRate;
        }
    }
}

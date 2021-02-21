using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Weapon")]
public class WeaponData : ScriptableObject
{
    public Projectile Projectile;
    public float fireRate;
}
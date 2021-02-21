using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Projectile")]
public class ProjectileData : ScriptableObject
{
    public Sprite sprite;
    public int damage;
    public float speed;
    public float range;
    public float timeToLive;
}

using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Enemy")]
public class EnemyData : ScriptableObject, IHealthData
{
    public string displayName;
    public int hitPoints;
    public int damage;
    public Sprite sprite;
    public Color spriteColor;
    public float speed;
    public float scanRadius;

    public int HitPoints => hitPoints;
}
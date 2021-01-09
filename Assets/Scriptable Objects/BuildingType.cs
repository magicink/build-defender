using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Building")]
public class BuildingType : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public Sprite icon;
    public ResourceGeneratorData ResourceGeneratorData;
    public float range = 5.0f;
}
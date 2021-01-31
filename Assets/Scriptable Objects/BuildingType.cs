using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Building")]
public class BuildingType : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public Sprite icon;
    public ResourceGeneratorData resourceGeneratorData;
    public float range = 5.0f;
    public int maxNodes = 5;
    public ConstructionCosts constructionCosts;
}
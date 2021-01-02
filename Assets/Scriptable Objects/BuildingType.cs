using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Building")]
public class BuildingType : ScriptableObject
{
    public string displayName;
    public GameObject prefab;
    public ResourceGeneratorData ResourceGeneratorData;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Construction Cost")]
public class ConstructionCost : ScriptableObject
{
    public ResourceData resourceType;
    public int amount;
}
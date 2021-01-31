using UnityEngine;

[CreateAssetMenu(menuName = "Build Defender/Construction Cost")]
public class ConstructionCost : ScriptableObject
{
    public ResourceType resourceType;
    public int amount;
}
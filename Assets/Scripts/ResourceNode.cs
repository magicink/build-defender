using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;

    public ResourceType ResourceType => _resourceType;
}

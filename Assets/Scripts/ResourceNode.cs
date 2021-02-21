using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceData _resourceType;

    public ResourceData ResourceType => _resourceType;
}

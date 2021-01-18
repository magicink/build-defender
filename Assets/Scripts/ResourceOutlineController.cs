using Shaders;
using UnityEngine;

[RequireComponent(typeof(ResourceNode))]
[RequireComponent(typeof(OutlineController))]
public class ResourceOutlineController : MonoBehaviour
{
    private OutlineController _outlineController;
    private ResourceNode _resourceNode;
    private void Awake()
    {
        _outlineController = GetComponent<OutlineController>();
        _resourceNode = GetComponent<ResourceNode>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleTrigger(other, true);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        HandleTrigger(other, false);
    }

    private void HandleTrigger(Collider2D other, bool outlined)
    {
        var buildingGhost = other.GetComponent<BuildingGhost>();
        if (buildingGhost)
        {
            var buildingType = buildingGhost.BuildingType;
            if (buildingType)
            {
                if (buildingType.ResourceGeneratorData.ResourceType == _resourceNode.ResourceType)
                {
                    _outlineController.Outlined = outlined;
                }
            }
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(BuildingData))]
public class ResourceGenerator : MonoBehaviour
{
    public delegate void OnResourceGeneration(ResourceType resourceType, ResourceGenerator generator);

    public OnResourceGeneration HandleResourceGeneration;
    
    [SerializeField] private float timeToLive;
    [SerializeField] private float maxTimeToLive = 1.0f;
    [SerializeField] private BuildingData buildingData;

    private BuildingType _buildingType;
    private ResourceType _resourceType;

    private void Awake()
    {
        buildingData = GetComponent<BuildingData>();
        if (buildingData)
        {
            _buildingType = buildingData.BuildingType;
            _resourceType = _buildingType.ResourceGeneratorData.ResourceType;
            maxTimeToLive = _buildingType.ResourceGeneratorData.MaxTimeToLive;
        }
        timeToLive = maxTimeToLive;
        ResourceManager.Instance.AddListener(this);
    }

    private void Start()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, _buildingType.range);
        foreach (var other in results)
        {
            var resourceNode = other.gameObject.GetComponent<ResourceNode>();
            if (!resourceNode) continue;
            if (resourceNode.ResourceType == _buildingType.ResourceGeneratorData.ResourceType)
            {
                Debug.Log(resourceNode);
            }
        }
    }

    private void Update()
    {
        if (!_buildingType || !_resourceType) return;
        timeToLive -= Time.deltaTime;
        if (!(timeToLive <= 0)) return;
        HandleResourceGeneration?.Invoke(_resourceType, this);
        timeToLive = maxTimeToLive;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.RemoveListener(this);
    }
}

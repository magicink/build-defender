using UnityEngine;

[RequireComponent(typeof(BuildingDataController))]
public class ResourceGenerator : MonoBehaviour
{
    public delegate void OnResourceGeneration(ResourceData resourceType, ResourceGenerator generator);

    public OnResourceGeneration HandleResourceGeneration;
    
    [SerializeField] private float timeToLive;
    [SerializeField] private float maxTimeToLive = 1.0f;
    [SerializeField] private BuildingDataController buildingDataController;

    private BuildingData _buildingType;
    private ResourceData _resourceType;

    public int TotalNodes { get; private set; }

    private void Awake()
    {
        buildingDataController = GetComponent<BuildingDataController>();
        if (buildingDataController)
        {
            _buildingType = buildingDataController.BuildingType;
            _resourceType = _buildingType.resourceGeneratorData.ResourceType;
            maxTimeToLive = _buildingType.resourceGeneratorData.MaxTimeToLive;
        }
        timeToLive = maxTimeToLive;
    }

    private void Start()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, _buildingType.range);
        foreach (var other in results)
        {
            var resourceNode = other.gameObject.GetComponent<ResourceNode>();
            if (!resourceNode) continue;
            if (resourceNode.ResourceType == _buildingType.resourceGeneratorData.ResourceType)
            {
                TotalNodes = Mathf.Clamp(TotalNodes + 1, 0, _buildingType.maxNodes);
            }
        }
        ResourceManager.Instance.AddAccumulationListener(this);
    }

    private void Update()
    {
        if (!_buildingType || !_resourceType) return;
        if (TotalNodes < 1) return;
        timeToLive -= Time.deltaTime;
        if (!(timeToLive <= 0)) return;
        HandleResourceGeneration?.Invoke(_resourceType, this);
        timeToLive = maxTimeToLive;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.RemoveAccumulationListener(this);
    }
}

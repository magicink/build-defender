using UnityEngine;

[RequireComponent(typeof(BuildingData))]
public class ResourceGenerator : MonoBehaviour
{
    public delegate void OnResourceGeneration(ResourceType resourceType, ResourceGenerator generator);

    public OnResourceGeneration HandleResourceGeneration;
    
    [SerializeField] private float timeToLive;
    [SerializeField] private float maxTimeToLive = 1.0f;
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private int currentHitPoints;
    [SerializeField] private int maxHitPoints;

    private BuildingType _buildingType;
    private ResourceType _resourceType;

    public int TotalNodes { get; private set; }

    public int CurrentHitPoints
    {
        get => currentHitPoints;
        private set => currentHitPoints = value;
    }

    public int MaxHitPoints
    {
        get => maxHitPoints;
        private set => maxHitPoints = value;
    }

    private void Awake()
    {
        buildingData = GetComponent<BuildingData>();
        if (buildingData)
        {
            _buildingType = buildingData.BuildingType;
            _resourceType = _buildingType.resourceGeneratorData.ResourceType;
            maxTimeToLive = _buildingType.resourceGeneratorData.MaxTimeToLive;
            CurrentHitPoints = MaxHitPoints = _buildingType.startingHitPoints;
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

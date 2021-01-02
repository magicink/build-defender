using UnityEngine;

[RequireComponent(typeof(BuildingData))]
public class ResourceGenerator : MonoBehaviour
{
    public delegate void OnResourceGeneration(ResourceType resourceType);

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

    private void Update()
    {
        if (!_buildingType || !_resourceType) return;
        timeToLive -= Time.deltaTime;
        if (!(timeToLive <= 0)) return;
        HandleResourceGeneration?.Invoke(_resourceType);
        timeToLive = maxTimeToLive;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.RemoveListener(this);
    }
}

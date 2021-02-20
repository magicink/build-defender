using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private BuildingData buildingType;
    [SerializeField] private Canvas ui;
    [SerializeField] private TextMeshProUGUI label;

    private CircleCollider2D _circleCollider2D;

    public BuildingData BuildingType => buildingType;

    private void Awake()
    {
        if (ghost)
        {
            ghost.enabled = false;
        }

        _circleCollider2D = GetComponent<CircleCollider2D>();
        if (_circleCollider2D) _circleCollider2D.enabled = false;
    }

    private void Start()
    {
        BuildingManager.Instance.HandleCurrentBuildingChanged += HandleCurrentBuildingChanged;
    }

    private void HandleCurrentBuildingChanged([CanBeNull] BuildingData nextBuildingType)
    {
        if (!ghost) return;
        if (nextBuildingType)
        {
            buildingType = nextBuildingType;
            ghost.sprite = buildingType.icon;
            ghost.enabled = true;
            if (!_circleCollider2D) return;
            _circleCollider2D.enabled = true;
            _circleCollider2D.radius = buildingType.range;
        }
        else
        {
            buildingType = null;
            ghost.enabled = false;
            if (!_circleCollider2D) return;
            _circleCollider2D.enabled = false;
            _circleCollider2D.radius = 0.0f;
        }
    }

    private void Update()
    {
        transform.position = Utils.GetMousePosition();
        ghost.enabled = buildingType && !EventSystem.current.IsPointerOverGameObject();
        if (ui) ui.enabled = ghost.enabled;
        if (!label || !ghost.enabled) return;
        var resourceType = buildingType.resourceGeneratorData.ResourceType;
        var result = Physics2D.OverlapCircleAll(transform.position, buildingType.range);
        var count = (from c in result select c.GetComponent<ResourceNode>() into rn where rn select rn.ResourceType).Count(rt => rt == resourceType);
        count = Mathf.Clamp(count, 0, buildingType.maxNodes);
        label.text = $"{count}/{buildingType.maxNodes}";
    }

    private void OnDrawGizmos()
    {
        if (ghost.enabled && buildingType)
        {
            Gizmos.color = new Color(1,0,0,.1f);
            Gizmos.DrawSphere(Utils.GetMousePosition(), buildingType.range);
        }
    }
}

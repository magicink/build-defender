using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private BuildingType buildingType;

    private CircleCollider2D _circleCollider2D;

    public BuildingType BuildingType => buildingType;

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

    private void HandleCurrentBuildingChanged(BuildingType nextBuildingType)
    {
        if (!ghost) return;
        if (nextBuildingType != null)
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

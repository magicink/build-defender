using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghost;
    [SerializeField] private BuildingType buildingType;

    private void Awake()
    {
        if (ghost)
        {
            ghost.enabled = false;
        }
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
        }
        else
        {
            ghost.enabled = false;
        }
    }

    private void Update()
    {
        transform.position = Utils.GetMousePosition();
        ghost.enabled = buildingType && !EventSystem.current.IsPointerOverGameObject();
    }
}

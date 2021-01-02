using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetBuildingType(BuildingType buildingType)
    {
        if (!spriteRenderer) return;
        Instantiate(spriteRenderer, transform);
    }
}

using UnityEngine;

public class BuildingData : MonoBehaviour
{
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private bool isHeadquarters;

    public BuildingType BuildingType => buildingType;

    private void Start()
    {
        if (isHeadquarters)
        {
            BuildingManager.Instance.SetHeadquarters(gameObject);
        }
    }
}

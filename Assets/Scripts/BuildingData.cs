using UnityEngine;

public class BuildingData : MonoBehaviour
{
    [SerializeField] private BuildingType buildingType;

    public BuildingType BuildingType => buildingType;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private Image resourceIcon;
    // Start is called before the first frame update
    void Start()
    {
        if (buildingData && resourceIcon)
        {
            resourceIcon.sprite = buildingData.BuildingType.resourceGeneratorData.ResourceType.icon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

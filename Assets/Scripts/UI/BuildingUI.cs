using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private BuildingSelection selectionPrefab;

    private void Awake()
    {
        if (!selectionPrefab) return;
        var source = Resources.Load<BuildingTypes>(nameof(BuildingTypes));
        if (!source) return;
        if (source.data.Count <= 0) return;
        var offset = 60;
        foreach (var buildingType in source.data)
        {
            var instance = Instantiate(selectionPrefab, transform);
            instance.transform.position = new Vector3(offset, 60, 0);
            instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 60);
            offset += 110;
            instance.GetComponent<BuildingSelection>().BuildingType = buildingType;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

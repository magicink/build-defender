using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private BuildingSelection selectionPrefab;

    private void Awake()
    {
        if (!selectionPrefab) return;
        var source = Resources.Load<BuildingCollection>(nameof(BuildingCollection));
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
}

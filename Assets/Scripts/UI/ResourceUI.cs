using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceMeter meterPrefab;

    private void Awake()
    {
        if (!meterPrefab) return;
        var source = Resources.Load<ResourceTypes>(nameof(ResourceTypes));
        if (!source) return;
        if (source.data.Count <= 0) return;
        var offset = 0;
        foreach (var resourceType in source.data)
        {
            var instance = Instantiate(meterPrefab, transform);
            instance.transform.position = new Vector3(offset, 0, 0);
            instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 0);
            offset -= 100;
            instance.GetComponent<ResourceMeter>().ResourceType = resourceType;
        }
    }
}

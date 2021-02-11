using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceLabel;
    [SerializeField] private Image healthBar;

    private ResourceGenerator _resourceGenerator;
    private HealthManager _healthManager;
    public Gradient gradient;

    private void Start()
    {
        if (!buildingData || !resourceIcon) return;
        _resourceGenerator = buildingData.GetComponent<ResourceGenerator>();
        resourceIcon.sprite = buildingData.BuildingType.resourceGeneratorData.ResourceType.icon;
        if (!_resourceGenerator) return;
        _healthManager = _resourceGenerator.GetComponent<HealthManager>();
    }

    private void Update()
    {
        if (_resourceGenerator && resourceLabel)
        {
            resourceLabel.text = $"{_resourceGenerator.TotalNodes} / {buildingData.BuildingType.maxNodes}";
        }

        if (!_healthManager || !healthBar) return;
        var healthPercentage =
            Mathf.Clamp((float) _healthManager.CurrentHitPoints / _healthManager.MaxHitPoints, 0, 1);
        healthBar.color = gradient.Evaluate(healthPercentage);
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
    }
}

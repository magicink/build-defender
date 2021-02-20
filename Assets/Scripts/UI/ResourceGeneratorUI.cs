using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGeneratorUI : MonoBehaviour
{
    [SerializeField] private BuildingDataController buildingDataController;
    [SerializeField] private Image resourceIcon;
    [SerializeField] private TextMeshProUGUI resourceLabel;
    [SerializeField] private Image healthBar;

    private ResourceGenerator _resourceGenerator;
    private HealthController _healthController;
    public Gradient gradient;

    private void Start()
    {
        if (!buildingDataController || !resourceIcon) return;
        _resourceGenerator = buildingDataController.GetComponent<ResourceGenerator>();
        resourceIcon.sprite = buildingDataController.BuildingType.resourceGeneratorData.ResourceType.icon;
        if (!_resourceGenerator) return;
        _healthController = _resourceGenerator.GetComponent<HealthController>();
    }

    private void Update()
    {
        if (_resourceGenerator && resourceLabel)
        {
            resourceLabel.text = $"{_resourceGenerator.TotalNodes} / {buildingDataController.BuildingType.maxNodes}";
        }

        if (!_healthController || !healthBar) return;
        var healthPercentage =
            Mathf.Clamp((float) _healthController.CurrentHitPoints / _healthController.MaxHitPoints, 0, 1);
        healthBar.color = gradient.Evaluate(healthPercentage);
        healthBar.rectTransform.localScale = new Vector3(healthPercentage, 1, 1);
    }
}

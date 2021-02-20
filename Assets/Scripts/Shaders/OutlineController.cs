using System.Collections.Generic;
using UnityEngine;

namespace Shaders
{
    public class OutlineController : MonoBehaviour
    {
        [SerializeField] private bool outlined;
        [SerializeField] [Range(0, 3)] private float thickness;
        [SerializeField] private float speed = 0.25f;

        public bool Outlined
        {
            get => outlined;
            set => outlined = value;
        }

        private readonly List<SpriteRenderer> _outlines = new List<SpriteRenderer>();
        private float _currentThickness;
        private float _targetThickness;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                var childSprite = child.GetComponent<SpriteRenderer>();
                if (childSprite.material.HasProperty(OutlineMaterialProperties.OutlineThickness))
                {
                    _outlines.Add(childSprite);
                }
            }
        }

        private void Start()
        {
            BuildingManager.Instance.HandleCurrentBuildingChanged += HandleBuildingHasChanged;
        }

        private void HandleBuildingHasChanged(BuildingData buildingType)
        {
            Outlined = false;
        }

        private void Update()
        {
            if (_outlines.Count < 1) return;
            _targetThickness = Outlined ? thickness : 0.0f;
            if (_currentThickness < _targetThickness || _currentThickness > _targetThickness)
            {
                _currentThickness = Mathf.Lerp(_currentThickness, _targetThickness, Time.deltaTime * speed);
                _currentThickness = Mathf.Clamp(_currentThickness, 0, thickness);
                foreach (var outline in _outlines)
                {
                    outline.material.SetFloat(OutlineMaterialProperties.OutlineThickness, _currentThickness);
                }
            }
        }
    }
}
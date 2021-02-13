using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrderController : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private Transform root;
    [SerializeField] private float sortPrecision = 5.0f;
    [SerializeField] private List<SpriteRenderer> sprites;

    private void Start()
    {
        if (!root) root = transform.parent;
    }

    private void LateUpdate()
    {
        if (root)
        {
            var next = 0.0f;
            for (var i = sprites.Count - 1; i >= 0; i--)
            {
                var rootPosition = root.transform.position;
                var baseSort = -(rootPosition.y * sortPrecision) - next;
                next++;
                sprites[i].sortingOrder = (int) baseSort;
            }
        }
        if (runOnce) Destroy(this);
    }
}
using Cinemachine;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float _orthographicSize;
    private float _targetOrthographicSize;
    public PanOptions PanOptions;
    public ZoomOptions zoomOptions;

    private void Start()
    {
        if (virtualCamera)
        {
            _orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    private void Update()
    {
        HandlePosition();
        HandleZoom();
    }

    private void HandlePosition()
    {
        var x =Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(x, y).normalized;
        transform.position += direction * (PanOptions.speed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        if (!virtualCamera) return;
        var nextOrthographicSize = Mathf.Clamp(_targetOrthographicSize + Input.mouseScrollDelta.y * -zoomOptions.steps, zoomOptions.minSize, zoomOptions.maxSize);
        _targetOrthographicSize = nextOrthographicSize;
        _orthographicSize = Mathf.Lerp(_orthographicSize, _targetOrthographicSize, Time.deltaTime * zoomOptions.speed);
        virtualCamera.m_Lens.OrthographicSize = _orthographicSize;
    }
}
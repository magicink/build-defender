using UnityEngine;

public static class Utils
{
    private static Camera _mainCamera;

    public static Vector3 GetMousePosition()
    {
        if (!_mainCamera) _mainCamera = Camera.main;
        if (_mainCamera == null) return Vector3.zero;
        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;
        return mousePosition;
    }
}
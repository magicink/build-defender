using UnityEngine;
using Random = UnityEngine.Random;

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

    public static float GetAngle(Vector3 vector)
    {
        var radians = Mathf.Atan2(vector.y, vector.x);
        var degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
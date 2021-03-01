using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float maxTimeToLive = 0.25f;

    public static CameraShake Instance;

    private CinemachineBasicMultiChannelPerlin perlin;
    private float _timeToLive;
    private bool _hasPerlin;
    private float _intensity;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (virtualCamera)
        {
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (perlin) _hasPerlin = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_hasPerlin && _timeToLive > 0)
        {
            var amplitude = Mathf.Lerp(_intensity, 0f, _timeToLive / maxTimeToLive);
            perlin.m_AmplitudeGain = amplitude;
            _timeToLive -= Time.deltaTime;
        }
        else
        {
            virtualCamera.transform.rotation = Quaternion.identity;
            perlin.m_AmplitudeGain = 0;
            _intensity = 0;
        }
    }

    public void StartShake(float intensity)
    {
        _intensity = intensity;
        _timeToLive = maxTimeToLive;
    }
}

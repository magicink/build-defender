using UnityEngine;

public class EnemyDataController : MonoBehaviour, IHealthDataProvider
{
    [SerializeField] private EnemyData data;

    public EnemyData Data => data;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public IHealthData HealthData => data;
}

using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPointController : MonoBehaviour
{
    public List<Enemy> prefabs;

    public int spawnCount = 20;
    public int seed;

    private static void CreateEnemy(Enemy prefab, Vector3 position)
    {
        Instantiate(prefab, position, quaternion.identity);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (prefabs.Count > 0)
        {
            for (var i = 0; i < Random.Range(spawnCount / 2, spawnCount); i++)
            {
                var index = Random.Range(0, prefabs.Count - 1);
                var prefab = prefabs[index];
                CreateEnemy(prefab, transform.position+Utils.GetRandomDirection());
            }
        }
    }
}

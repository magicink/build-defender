using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnPointController : MonoBehaviour
{
    public List<Enemy> prefabs;

    public int spawnCount;
    public float spawnDelay = 0.15f;
    public float timer;
    public float cooldown;
    public int booster;
    public float minCooldown = 30f;
    public float maxCooldown = 120f;

    private static void CreateEnemy(Enemy prefab, Vector3 position)
    {
        Instantiate(prefab, position, quaternion.identity);
    }

    private void Awake()
    {
        cooldown = Random.Range(30f, 60f);
    }

    private void Update()
    {
        if (prefabs.Count <= 0) return;
        if (!BuildingManager.Instance.Headquarters) return;
        if (spawnCount == 0 && cooldown <= 0)
        {
            cooldown = Random.Range(minCooldown, maxCooldown);
            spawnCount = Random.Range(10, 15) + booster;
            booster += 5;
            minCooldown = Mathf.Clamp(minCooldown - 1f, 10f, 30f);
            maxCooldown = Mathf.Clamp(maxCooldown - 1f, 30f, 120f);
            return;
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        if (timer <= 0)
        {
            var prefab = prefabs[Random.Range(0, 100) % 2];
            CreateEnemy(prefab, transform.position + Utils.GetRandomDirection() * Random.Range(1, 10));
            timer = spawnDelay;
            spawnCount -= 1;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Components")]
    [SerializeField] private List<GameObject> enemies;
    [Header("Parameters")]
    [SerializeField] private float enemySpawnInterval = 1f;

    private int aliveEnemies = 0;
    private Transform spawnPoint;

    public Action OnWaveEnd;

    public void Initialize(Transform spawn)
    {
        spawnPoint = spawn;
    }

    public IEnumerator RunWave()
    {
        Debug.Log($"Wave starting. Enemy count: {enemies.Count}");
        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log($"Enemy prefab in list [{i}]: {enemies[i].name}");
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            var enemyPrefab = enemies[i];

            var enemyInstance = GameObject.Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            if (enemyInstance == null)
            {
                Debug.LogError($"Failed to spawn enemy: {enemyPrefab.name}");
                continue;
            }

            aliveEnemies++;

            var damageable = enemyInstance.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.OnDeath += HandleEnemyDeath;
            }
            else
            {
                Debug.LogWarning($"Enemy {enemyInstance.name} has no IDamageable component!");
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(enemySpawnInterval - 0.25f, enemySpawnInterval + 0.25f));
        }

        yield return new WaitUntil(() => aliveEnemies <= 0);

        OnWaveEnd?.Invoke();
    }

    private void HandleEnemyDeath()
    {
        aliveEnemies--;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [Header("Components")]
    [SerializeField] List<GameObject> enemies;
    [Header("Parameters")]
    [SerializeField] float enemySpawnInterval;

    private int aliveEnemies = 0;

    public event Func<GameObject, GameObject> OnEnemySpawnRequested;
    public event Action OnWaveEnd;

    public IEnumerator RunWave()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var enemyPrefab = enemies[i];
            var enemyInstance = OnEnemySpawnRequested?.Invoke(enemyPrefab);

            aliveEnemies++;

            enemyInstance.GetComponent<IDamageable>().OnDeath += HandleEnemyDeath;

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
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] List<Wave> waves;
    [SerializeField] Transform spawnPoint;

    private int currentWaveIndex = 0;

    private void Start()
    {
        if (waves.Count > 0)
        {
            StartWave(waves[currentWaveIndex]);
        }
    }

    private void StartWave(Wave wave)
    {
        wave.OnWaveEnd += HandleWaveEnd;
        wave.OnEnemySpawnRequested += SpawnEnemy;

        StartCoroutine(wave.RunWave());
    }

    private GameObject SpawnEnemy(GameObject prefab)
    {
        return Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }

    private void HandleWaveEnd()
    {
        waves[currentWaveIndex].OnWaveEnd -= HandleWaveEnd;
        waves[currentWaveIndex].OnEnemySpawnRequested -= SpawnEnemy;

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Count)
        {
            EndBattle();
        }
        else
        {
            StartWave(waves[currentWaveIndex]);
        }
    }

    private void EndBattle()
    {
        Debug.Log("End Battle");
    }
}
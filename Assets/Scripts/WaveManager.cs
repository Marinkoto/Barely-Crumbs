using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform spawnPoint;

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
        wave.Initialize(spawnPoint);
        wave.OnWaveEnd += HandleWaveEnd;
        StartCoroutine(wave.RunWave());
        Debug.Log($"Wave {currentWaveIndex + 1} started");
    }

    private void HandleWaveEnd()
    {
        waves[currentWaveIndex].OnWaveEnd -= HandleWaveEnd;

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
        Debug.Log("All waves completed! Battle ended.");
    }
}

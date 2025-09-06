using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [Header("Components")]
    [SerializeField] private List<Wave> waves;
    [SerializeField] private Transform spawnPoint;

    public static Action<int> OnWaveEnd;
    public static Action OnBattleEnd;
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

        if (currentWaveIndex == waves.Count - 1)
        {
            if (waves[currentWaveIndex].aliveEnemies <= 0)
            {
                EndBattle();
            }
        }
        else
        {
            currentWaveIndex++;
            StartWave(waves[currentWaveIndex]);
            OnWaveEnd?.Invoke(currentWaveIndex);
        }
    }

    private void EndBattle()
    {
        OnBattleEnd?.Invoke();
    }
}

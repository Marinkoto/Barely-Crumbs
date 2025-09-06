using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject endScreen;
    private void OnEnable()
    {
        WaveManager.OnWaveEnd += DisplayWave;
        WaveManager.OnBattleEnd += EndScreenWinDisplay;
        TowerLayer.OnTopLayerDestroyed += EndScreenLoseDisplay;
    }
    private void OnDisable()
    {
        WaveManager.OnWaveEnd -= DisplayWave;
        WaveManager.OnBattleEnd -= EndScreenWinDisplay;
        TowerLayer.OnTopLayerDestroyed -= EndScreenLoseDisplay;
    }
    private void DisplayWave(int wave)
    {
        waveText.text = $"Wave: {wave + 1}";
    }
    private void EndScreenWinDisplay()
    {
        waveText.text = "You won, with barely crumbs!";
        Time.timeScale = 0;
        CharacterEffects.Instance.hitEffect.SetActive(false);
        endScreen.SetActive(true);
    }
    private void EndScreenLoseDisplay()
    {
        waveText.text = "You are left with no crumbs!";
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }
}

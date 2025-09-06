using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] GameObject deathEffect;

    private void OnEnable()
    {
        health.OnDeath += (d) => SpawnEffect();
    }
    private void OnDestroy()
    {
        health.OnDeath -= (d) => SpawnEffect();
    }
    private void SpawnEffect()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }
}

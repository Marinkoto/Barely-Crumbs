using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] float hitCooldown = 0.75f;
    float lastHitTime;
    public Action<IDamageable> OnDeath { get; set; }
    public Action<int> OnDamage { get; set; }

    public virtual void TakeDamage(int amount)
    {
        if (Time.time - lastHitTime < hitCooldown) return;

        lastHitTime = Time.time;
        health -= amount;
        OnDamage?.Invoke(amount);
        if (health <= 0)
            Die();
    }
    public virtual void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
        EnemyManager.Enemies.Remove(transform);
    }
}

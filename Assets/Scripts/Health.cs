using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Parameters")]
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;

    public Action OnDeath { get; set; }
    public Action<int> OnDamage { get; set; }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnDamage?.Invoke(amount);
        if (health <= 0)
            Die();
    }
    protected virtual void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}

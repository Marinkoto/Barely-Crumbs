using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] Slider healthBar;
    [Header("Sounds")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        EnemyManager.Enemies.Add(transform);
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        animator.SetTrigger("Hit");
        AudioSource.PlayClipAtPoint(hit, transform.position);
        healthBar.value = health;
        healthBar.maxValue = maxHealth;
    }
    public override void Die()
    {
        base.Die();
        AudioSource.PlayClipAtPoint(death, transform.position);
    }
}

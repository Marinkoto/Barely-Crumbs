using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerLayer : Health, IDamageable
{
    [Header("Parameters")]
    [SerializeField] AbilityBullet ability;
    [Header("Components")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Image layerImage;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        float alpha = Mathf.Clamp01((float)health / maxHealth);
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
        layerImage.color = c;
    }
    public override void Die()
    {
        base.Die();
        Destroy(layerImage.gameObject);
    }
    public void AssignLayerProjectile()
    {
        if (PlayerAbilities.Instance.HasAbility())
            return;

        PlayerAbilities.Instance.AssignAbility(ability);
        TakeDamage(5);
        if (health <= maxHealth * 0.35f)
        {
            Die();
        }
    }
}

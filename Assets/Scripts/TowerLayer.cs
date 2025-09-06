using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerLayer : Health
{
    [Header("Parameters")]
    [SerializeField] AbilityBullet ability;
    [SerializeField] bool isTopLayer;
    [Header("Components")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Image layerImage;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip layerClip;

    public static Action OnTopLayerDestroyed;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        float alpha = Mathf.Clamp01((float)health / maxHealth);
        StartCoroutine(CharacterEffects.Instance.HitEffect());
        AudioManager.Instance.PlaySound(source, layerClip, true);
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
        if (isTopLayer || layerImage == null)
            return;
        layerImage.color = c;
    }
    public override void Die()
    {
        base.Die();
        if (isTopLayer)
        {
            WaveManager.Instance.StopAllCoroutines();
            OnTopLayerDestroyed?.Invoke();
            return;
        }
        Destroy(layerImage.gameObject);
        CharacterEffects.Instance.hitEffect.SetActive(false);
        AudioSource.PlayClipAtPoint(layerClip, transform.position);
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

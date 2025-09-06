using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AbilityType { Cookie, Cream };
public class AbilityEffect : MonoBehaviour
{
    public Action EffectActivated;
    public Action EffectDeactivated;
    public AbilityType type;
    public LayerMask enemyLayer;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip effectInitialize;
    private void Start()
    {
        AudioManager.Instance.PlaySound(source, effectInitialize, true);
        switch (type)
        {
            case AbilityType.Cookie:
                {
                    EffectActivated = () =>
                    {
                        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1, enemyLayer);
                        if (hits.Length >= 1)
                        {
                            foreach (var hit in hits)
                            {
                                var enemy = hit.GetComponent<Health>();
                                if (enemy != null)
                                {
                                    enemy.TakeDamage(20);
                                }
                            }
                        }
                    };
                    EffectDeactivated = () =>
                    {
                        Destroy(gameObject, 10f);
                    };
                }
                break;
            case AbilityType.Cream:
                {
                    EffectActivated = () =>
                    {
                        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2, enemyLayer);
                        if (hits.Length >= 1)
                        {
                            foreach (var hit in hits)
                            {
                                var enemy = hit.GetComponent<EnemyMovement>();
                                enemy.ReduceMoveSpeed();
                            }
                        }
                    };
                    EffectDeactivated = () =>
                    {
                        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1, enemyLayer);
                        if (hits.Length >= 1)
                        {
                            foreach (var hit in hits)
                            {
                                var enemy = hit.GetComponent<EnemyMovement>();
                                if (enemy != null)
                                {
                                    enemy.ResetMoveSpeed();
                                }
                            }
                        }
                        Destroy(gameObject, 10f);
                    };
                }
                break;
        }
    }
    public void ActivateEffect() => EffectActivated.Invoke();
    public void DeactivateEffect() => EffectDeactivated.Invoke();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ActivateEffect();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DeactivateEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out TowerLayer layer))
        {
            layer.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

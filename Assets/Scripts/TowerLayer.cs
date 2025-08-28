using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerLayer : Health, IDamageable
{
    [Header("Parameters")]
    [SerializeField] AbilityBullet ability;

    public void AssignLayerProjectile()
    {
        PlayerAbilities.Instance.AssignAbility(ability);
        TakeDamage(5);
    }
}

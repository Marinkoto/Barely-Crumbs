using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    Action<IDamageable> OnDeath { get; set; }
    Action<int> OnDamage { get; set; }
    public void TakeDamage(int amount);
}

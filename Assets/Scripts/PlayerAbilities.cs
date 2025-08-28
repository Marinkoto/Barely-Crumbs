using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : Singleton<PlayerAbilities>
{
    [Header("Components")]
    [HideInInspector] public AbilityBullet currentAbility;
    [SerializeField] Transform abilityPosition;
    [SerializeField] TrajectoryDrawer trajectoryDrawer;

    public void AssignAbility(AbilityBullet ability)
    {
        if (HasAbility())
            return;

        currentAbility = Instantiate(ability, abilityPosition.position, Quaternion.identity);
        trajectoryDrawer.active = true;
    }
    public bool HasAbility()
    {
        return currentAbility != null;
    }
}

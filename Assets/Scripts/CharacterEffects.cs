using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterEffects : Singleton<CharacterEffects>
{
    [Header("Components")]
    [SerializeField] public GameObject hitEffect;
    [SerializeField] GameObject slowMoEffect;

    public IEnumerator HitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        hitEffect.SetActive(false);
    }
    public void SlowMoEffect(bool state)
    {
        slowMoEffect.SetActive(state);
    }
}

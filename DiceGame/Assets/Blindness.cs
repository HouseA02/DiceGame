using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindness : StatusEffect
{
    public override void Initialise(Character character, int newValue)
    {
        base.Initialise(character, newValue);
        characterReference.m_OnAbilityUsed.AddListener(OnAbilityUsed);
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        value--;
        UpdateValue();
    }

    public void OnAbilityUsed(Ability ability)
    {
        if(ability.targetingType == Ability.TargetingType.single)
        {
            TriggerEffect();
            UpdateValue();
        }
    }
    public override void OnApplied()
    {
        base.OnApplied();
        characterReference.isBlind = true;
    }
    public override void OnExpire()
    {
        characterReference.isBlind = false;
        base.OnExpire();
    }
}
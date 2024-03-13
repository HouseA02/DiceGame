using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : StatusEffect
{

    public override void Initialise(Character character, int newValue)
    {
        base.Initialise(character, newValue);
        characterReference.m_OnAttacked.AddListener(OnAttacked);
    }
    public override void OnApplied()
    {
        base.OnApplied();
        characterReference.damageMultiplier = 0;
    }

    public override void OnTurnStart()
    {
        value--;
        slot.UpdateValue(value);
        if (value <= 0) { Expire(); }
    }

    public void OnAttacked()
    {
        value--;
        TriggerEffect();
        slot.UpdateValue(value);
        if (value <= 0) { Expire(); }
    }
    public override void Expire()
    {
        characterReference.damageMultiplier = 1;
        base.Expire();
    }
}

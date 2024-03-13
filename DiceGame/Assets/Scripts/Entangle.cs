using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entangle : StatusEffect
{
    public override void Initialise(Character character, int newValue)
    {
        base.Initialise(character, newValue);
        characterReference.m_OnReroll.AddListener(OnReroll);
    }

    public void OnReroll()
    {
        value--;
        TriggerEffect();
        UpdateValue();
    }
    public override void OnApplied()
    {
        base.OnApplied();
        characterReference.canRoll = false;
    }
    public override void OnExpire()
    {
        characterReference.canRoll = true;
        base.OnExpire();
    }
}

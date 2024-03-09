using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : StatusEffect
{
    public override void Initialise(Character character, int newValue)
    {
        base.Initialise(character, newValue);
    }

    public override void OnApplied()
    {
        base.OnApplied();
        characterReference.power = value;
    }

    public override void UpdateValue()
    {
        characterReference.power = value;
        base.UpdateValue();
    }

    public override void OnExpire()
    {
        characterReference.power -= value;
        base.OnExpire();
    }
}

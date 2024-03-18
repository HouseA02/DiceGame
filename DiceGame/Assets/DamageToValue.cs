using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToValue : Effect
{
    public Effect effect;

    public override void Activate(Character source, Character target, float value)
    {
        effect.Activate(source, target, value * abilityReference.lastHitDamage);
        base.Activate(source, target, value);
    }
}

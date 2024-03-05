using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        target.TakeDamage((int)(value * source.power));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : Effect
{
    [SerializeField]
    int defaultDamage;
    public override void Activate(Character source, Character target, float value)
    {
        target.TakeDamage((int)(value + source.power));
    }

    public override void Activate(Character target, float value)
    {
        target.TakeDamage((int)value);
    }

    public override void Activate(Character target)
    {
        target.TakeDamage(defaultDamage);
    }
}

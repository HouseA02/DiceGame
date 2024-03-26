using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : Effect
{
    [SerializeField]
    int defaultDamage;
    public override void Activate(Character source, Character target, float value)
    {
        int preHP = target.HP;
        target.TakeDamage((int)(value + source.power), (target.transform.position - source.transform.position));
        ReturnDamage(preHP - target.HP);
        base.Activate(source, target, value);
    }

    public override void Activate(Character source, Character target)
    {
        target.TakeDamage(defaultDamage + source.power, (target.transform.position - source.transform.position));
        base.Activate(source, target);
    }

    public override void Activate(Character target, float value)
    {
        target.TakeDamage((int)value);
        base.Activate(target, value);
    }

    public override void Activate(Character target)
    {
        target.TakeDamage(defaultDamage);
        base.Activate(target);
    }

    public void ReturnDamage(int damage)
    {
        if(abilityReference != null)
        {
            abilityReference.lastHitDamage = damage;
            abilityReference.totalDamage += damage;
        }
    }
}

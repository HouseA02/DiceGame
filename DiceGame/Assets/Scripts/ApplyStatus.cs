using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatus : Effect
{
    [SerializeField]
    StatusEffect status;
    [SerializeField]
    private int defaultValue;
    public override void Activate(Character source, Character target, float value)
    {
        if (target.isDead == false) { target.ApplyStatus(status, (int)value); }
        base.Activate(source, target, value);
    }
    public override void Activate(Character target)
    {
        if (target.isDead == false) { target.ApplyStatus(status, defaultValue); } 
        base.Activate(target);
    }
}

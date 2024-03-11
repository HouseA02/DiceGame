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
        target.ApplyStatus(status, (int)value);
    }
    public override void Activate(Character target)
    {
        target.ApplyStatus(status, defaultValue);
    }
}

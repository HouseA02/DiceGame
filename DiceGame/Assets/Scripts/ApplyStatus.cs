using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatus : Effect
{
    [SerializeField]
    StatusEffect status;
    public override void Activate(Character source, Character target, float value)
    {
        target.ApplyStatus(status, (int)value);
    }
}

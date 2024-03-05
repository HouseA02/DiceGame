using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainStatus : Effect
{
    [SerializeField]
    StatusEffect status;
    public override void Activate(Character source, Character target, float value)
    {
        source.ApplyStatus(status, (int)value);
    }
}

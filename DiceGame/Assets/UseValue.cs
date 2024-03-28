using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseValue : Effect
{
    public Effect[] effects;
    public float value;

    public override void Activate()
    {
        value = GetComponent<Relic>().currentValue;
        foreach (Effect effect in effects) 
        {
            effect.Activate(value);
        }
        base.Activate();

    }
}

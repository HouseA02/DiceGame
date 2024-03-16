using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBlock : Effect
{
    [SerializeField]
    private int defaultValue;
    public override void Activate(Character source, Character target, float value)
    {
        target.ChangeBlock((int)value, false);
        base.Activate(source, target, value);
    }

    public override void Activate(Character target)
    {
        target.ChangeBlock(defaultValue, false);
        base.Activate(target);  
    }
}


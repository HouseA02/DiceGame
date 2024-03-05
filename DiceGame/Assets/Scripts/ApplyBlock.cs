using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyBlock : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        target.ChangeBlock((int)value, false);
    }
}


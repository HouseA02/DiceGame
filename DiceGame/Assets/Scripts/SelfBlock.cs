using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBlock : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        source.ChangeBlock((int)value, false);
    }
}

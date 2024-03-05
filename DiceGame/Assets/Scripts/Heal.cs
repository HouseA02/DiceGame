using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        target.ChangeHP((int)value);
        base.Activate(source, target, value);
    }
}

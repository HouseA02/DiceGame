using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffect
{
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        characterReference.ChangeBlock(-value, true);
        value /= 2;
        UpdateValue();
    }
}

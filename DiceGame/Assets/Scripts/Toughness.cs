using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toughness : StatusEffect
{
    public override void OnApplied()
    {
        characterReference.damageBlock += 1;
        base.OnApplied();
    }

    public override void OnExpire()
    {
        characterReference.damageBlock -= 1;
        base.OnExpire();
    }
}

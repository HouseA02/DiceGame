using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : StatusEffect
{

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        characterReference.TakeDamage(value);
        value /= 2;
        slot.UpdateValue(value);
        if(value <= 0) { Expire(); }
    }
}

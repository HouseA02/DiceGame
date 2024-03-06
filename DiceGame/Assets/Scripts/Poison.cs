using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        characterReference.ChangeHP(-value);
        value /= 2;
        UpdateValue();
    }
}

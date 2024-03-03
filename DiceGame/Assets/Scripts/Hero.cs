using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    bool hasActed = false;
    public override void Roll()
    {
        if (!hasActed)
        {
            base.Roll();
        }
    }

    public override void OnAbilityUsed()
    {
        base.OnAbilityUsed();
        hasActed = true;
    }

    public override void OnTurnStart()
    {
        hasActed = false;
        base.OnTurnStart();
    }
}

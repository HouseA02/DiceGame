using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnReroll : Relic
{
    public int value;
    public override void Initialise(GameManager gm)
    {
        base.Initialise(gm);
        gameManager.gm_OnReroll.AddListener(Reroll);
    }

    void Reroll()
    {
        foreach(AbilityEffect effect in effects)
        {
            effect.Activate();
        }
    }
}

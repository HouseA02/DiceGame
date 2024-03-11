using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEyes : Relic
{
    public int value;

    public override void Initialise(GameManager gm)
    {
        base.Initialise(gm);
        foreach (Hero hero in gameManager.activeHeroes)
        {
            hero.m_OnResult.AddListener(CheckResult);
            Debug.Log("Added");
        }
    }
    public void CheckResult(Ability ability)
    {
        Debug.Log("checked");
        if (ability != null && ability.abilityName == "Blank")
        {
            effects[0].Activate();
        }
    }
}

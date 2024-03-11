using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankReroll : Relic
{
    public bool canActivate = true;

    public override void Initialise(GameManager gm)
    {
        base.Initialise(gm);
        foreach (Hero hero in gameManager.activeHeroes)
        {
            hero.m_OnResult.AddListener(CheckResult);
            Debug.Log("Added");
        }
    }

    protected override void OnTurnStart()
    {
        canActivate = true;
    }
    public void CheckResult(Ability ability)
    {
        Debug.Log("checked");
        if (ability != null && ability.abilityName == "Blank" && canActivate)
        {
            ability.characterReference.Roll();
            canActivate = false;
        }
    }
}

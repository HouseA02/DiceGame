using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : Character
{
    public bool hasActed = false;
    public enum Class
    {
        None,
        Rogue,
        Knight,
        Mage
    }
    [SerializeField] 
    public Class m_Class;
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

    public override void OnTurnEnd()
    {
        hasActed = true;
        CleanUp();
        base.OnTurnEnd();
    }
}

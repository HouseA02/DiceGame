using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Hero : Character
{
    public bool hasActed = false;
    public GameObject parentModel;
    [Serializable]
    public enum Class
    {
        None,
        Enemy,
        Rogue,
        Knight,
        Mage
    }
    [SerializeField] 
    public List<Class> m_Class;
    public override void Roll()
    {
        if (!hasActed)
        {
            base.Roll();
        }
    }

    public override void Revive()
    {
        transform.position = gameManager.heroPositions[id].position;
        transform.rotation = gameManager.heroPositions[id].rotation;
        base.Revive();
    }
    public override void OnAbilityUsed(Ability ability)
    {
        base.OnAbilityUsed(ability);
        hasActed = true;
    }

    public override void OnTurnStart()
    {
        hasActed = false;
        transform.rotation = defaultRotation;
        base.OnTurnStart();
    }

    public override void OnTurnEnd()
    {
        hasActed = true;
        CleanUp();
        base.OnTurnEnd();
    }
}

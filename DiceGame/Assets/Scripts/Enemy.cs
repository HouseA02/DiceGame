using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : Character
{
    public Character target;
    [SerializeField]
    Quaternion defaultRotation;
    GameObject model;
    public override void SetAbility(int value)
    {
        base.SetAbility(value);
        if(currentAbility != null)
        {
            target = ChooseTarget(currentAbility);
            if (target != null) 
            { 
                target.characterPanel.targetSprite.gameObject.SetActive(true);
                target.targetSprite.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateTarget(Character newTarget)
    {
        if (target != null)
        {
            target.characterPanel.targetSprite.gameObject.SetActive(false);
            target.targetSprite.gameObject.SetActive(false);
        }
        target = newTarget;
        target.characterPanel.targetSprite.gameObject.SetActive(true);
        target.targetSprite.gameObject.SetActive(true);
    }
    public override void OnTurnEnd()
    {
        if(currentAbility != null)
        {
            currentAbility.Activate();
            if (target != null) 
            { 
                target.characterPanel.targetSprite.gameObject.SetActive(false);
                target.targetSprite.gameObject.SetActive(false);
            }
        }
        base.OnTurnEnd();
    }
    public Character ChooseTarget(Ability ability)
    {
        switch (ability.targetingType)
        {
            case Ability.TargetingType.single:
                if (ability.targetsEnemy)
                {
                    Character target = enemies[(int)Random.Range(0,enemies.Count)];
                    return target;
                }
                else
                {
                    Character target = allies[(int)Random.Range(0, allies.Count + 1)];
                    return target;
                }
            default: 
                return null;
        }
    }
}

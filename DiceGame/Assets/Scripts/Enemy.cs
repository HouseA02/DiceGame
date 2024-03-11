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
                //target.characterPanel.targetSprite.gameObject.SetActive(true);
                //target.targetSprite.gameObject.SetActive(true);
                transform.LookAt(target.transform.position);
                if(characterPanel.targetContainer != null)
                {
                    characterPanel.targetContainer.gameObject.SetActive(true);
                    characterPanel.targetImage.sprite = target.portrait;
                }
            }
            else
            {
                transform.LookAt(enemies[id].transform);
            }
        }
        else
        {
            transform.LookAt(enemies[id].transform);
        }
    }

    public override void CleanUp()
    {
        if (target != null && !CheckAllyTargets())
        {
            target.characterPanel.targetSprite.gameObject.SetActive(false);
            target.targetSprite.gameObject.SetActive(false);
            if (characterPanel.targetContainer != null)
            {
                characterPanel.targetContainer.gameObject.SetActive(false);
                characterPanel.targetImage.sprite = null;
            }
        }
        base.CleanUp();
    }

    bool CheckAllyTargets()
    {
        bool isCommonTarget=false;
        allies.ForEach(a =>
        {
            if (a.GetComponent<Enemy>().target == target)
            {
                isCommonTarget = true;
            }
        });
        return isCommonTarget;
    }
    public void UpdateTarget(Character newTarget)
    {
        if (target != null && !CheckAllyTargets())
        {
            target.characterPanel.targetSprite.gameObject.SetActive(false);
            target.targetSprite.gameObject.SetActive(false);
        }
        target = newTarget;
        //target.characterPanel.targetSprite.gameObject.SetActive(true);
        //target.targetSprite.gameObject.SetActive(true);
        if (characterPanel.targetContainer != null)
        {
            characterPanel.targetContainer.gameObject.SetActive(true);
            characterPanel.targetImage.sprite = target.portrait;
        }
    }
    public override void OnTurnStart()
    {
        base.OnTurnStart();
    }

    public void TakeAction()
    {
        if (currentAbility != null)
        {
            currentAbility.Activate();
            if (target != null)
            {
                target.characterPanel.targetSprite.gameObject.SetActive(false);
                target.targetSprite.gameObject.SetActive(false);
                if (characterPanel.targetContainer != null)
                {
                    characterPanel.targetContainer.gameObject.SetActive(false);
                    characterPanel.targetImage.sprite = null;
                }
            }
        }
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
                    Character target = allies[(int)Random.Range(0, allies.Count)];
                    return target;
                }
            default: 
                return null;
        }
    }

    public override void Die()
    {
        characterPanel.targetContainer.SetActive(false);
        base.Die();
    }
    public override void Initialise(Character temp)
    {
        instance = temp;
        HP = maxHP;
        damageMultiplier = 1;
        characterPanel.gameObject.SetActive(true);
        characterPanel.Initialise(this);
        gameManager.gm_OnTurnStart.AddListener(OnTurnEnd);
        gameManager.gm_OnTurnEnd.AddListener(OnTurnStart);
    }
}

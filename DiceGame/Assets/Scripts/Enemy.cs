using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy : Character
{
    public Character target;
    public Rigidbody mainRb;
    public Rigidbody[] rigidbodies;
    public Collider[] colliders;
    public Animator anim;

    protected override void Awake()
    {
        if (anim != null)
        {
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = true;
                rigidbody.detectCollisions = false;
            }
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
        base.Awake();

    }
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
            //if (gameManager.activeHeroes[id] != null && gameManager.activeHeroes[id].HP > 0) { transform.LookAt(gameManager.activeHeroes[id].transform); }
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
        if (characterPanel.targetContainer != null && target != null)
        {
            characterPanel.targetContainer.gameObject.SetActive(true);
            characterPanel.targetImage.sprite = target.portrait;
        }
        if(target == null)
        {
            characterPanel.targetContainer.gameObject.SetActive(false);
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
        if (anim != null)
        {
            anim.enabled = false;
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
                rigidbody.detectCollisions = true;
            }
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
                mainRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
        base.Die();
    }

    public override void Die(Vector3 dir)
    {
        characterPanel.targetContainer.SetActive(false);
        if(anim != null)
        {
            anim.enabled = false;
            foreach (Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
                rigidbody.detectCollisions = true;
            }
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
            mainRb.AddForce((dir * 10) + Vector3.up * 7, ForceMode.Impulse);
        }
        Debug.Log(dir);
        base.Die(dir);
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
        startingStatuses.ForEach(s => ApplyStatus(s.effect, s.value));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetedAbility : Ability
{
    [SerializeField]
    private List<Character> validTargets = new List<Character>();

    [SerializeField]
    bool isHealing;

    public override void Activate()
    {
        base.Activate();
        validTargets = GetTargets();
        Debug.Log(validTargets);
        if(characterReference.GetType() == typeof(Hero)) { StartCoroutine(WaitForInput()); }
        if (characterReference.CompareTag("Enemy")) { StartCoroutine(UseAbility(characterReference.GetComponent<Enemy>().target));  }
    }

    IEnumerator WaitForInput()
    {
        validTargets.ForEach(target => { target.indicator.SetActive(true); });
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1));
        validTargets.ForEach(target => { target.indicator.SetActive(false); });
        var ray = characterReference.gameManager.battleCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        int layermask = 1 << 9;
        if (Physics.Raycast(ray, out raycastHit, 100f, layermask))
        {
            Debug.Log("Attack");
            Character target = null;
            if (characterReference.isBlind)
            {
                List<Character> validEnemies = new List<Character>();
                validEnemies.AddRange(gameManager.activeEnemies.Where(e => e.isDead == false));
                target = validEnemies[Random.Range(0, validEnemies.Count)];
            }
            else
            {
                target = raycastHit.collider.GetComponentInParent<Character>();
            }
            Debug.Log(target);
            Debug.Log(raycastHit.transform.gameObject);
            if (validTargets.Contains(target))
            {
                characterReference.GetComponent<Hero>().hasActed = true;
                StartCoroutine(UseAbility(target));
            }
        }
    }

    /*public override void UseAbility(Character target)
    {
        var damage = (int)(characterReference.power * multiplier);
        if (isHealing) { 
            target.ChangeHP(damage);
        }
        else
        {
            target.TakeDamage(damage);
        }
        base.UseAbility(target);
    }*/
    public List<Character> GetTargets()
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemy)
        {
            foreach(Character e in characterReference.enemies.Where(e => e.targetable = true))
            {
                targets.Add(e);
            }
        }
        if (targetsAlly)
        {
            foreach(Character a in characterReference.allies.Where(a => a.targetable = true))
            {
                targets.Add(a);
            }
        }
        if (targetsSelf)
        {
            targets.Add(characterReference);
        }
        return targets;
    }
}

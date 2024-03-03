using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetedAbility : Ability
{
    [SerializeField]
    private List<Character> validTargets = new List<Character>();

    [SerializeField]
    float multiplier;
    [SerializeField]
    bool isHealing;

    public override void Activate()
    {
        base.Activate();
        validTargets = GetTargets();
        Debug.Log(validTargets);
        if(characterReference.GetType() == typeof(Hero)) { StartCoroutine(WaitForInput()); }
        if (characterReference.GetType() == typeof(Enemy)) { UseAbility(characterReference.GetComponent<Enemy>().target);  }
    }

    IEnumerator WaitForInput()
    {
        validTargets.ForEach(target => { target.indicator.SetActive(true); });
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
        validTargets.ForEach(target => { target.indicator.SetActive(false); });
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        int layermask = 1 << 9;
        if (Physics.Raycast(ray, out raycastHit, 100f, layermask))
        {
            Debug.Log("Attack");
            Character target = raycastHit.collider.GetComponentInParent<Character>();
            Debug.Log(target);
            Debug.Log(raycastHit.transform.gameObject);
            if (validTargets.Contains(target))
            {
                UseAbility(target);
            }
        }
    }

    void UseAbility(Character target)
    {
        var damage = (int)(characterReference.power * multiplier);
        if (isHealing) { 
            target.ChangeHP(damage);
        }
        else
        {
            target.TakeDamage(damage);
        }
        characterReference.CleanUp();
        characterReference.OnAbilityUsed();
    }
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
        return targets;
    }
}

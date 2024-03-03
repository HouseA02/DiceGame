using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetedAbility : Ability
{
    [SerializeField]
    private List<Character> validTargets = new List<Character>();
    [SerializeField]
    public bool targetsEnemy;
    [SerializeField]
    public bool targetsAlly;
    [SerializeField]
    float multiplier;
    [SerializeField]
    bool isHealing;

    public override void Activate()
    {
        base.Activate();
        validTargets = GetTargets();
        Debug.Log(validTargets);
        StartCoroutine(WaitForInput());
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
                var damage = -(int)(characterReference.power * multiplier);
                if(isHealing) { damage *= -1; }
                target.ChangeHP(damage);
                characterReference.CleanUp();
                characterReference.OnAbilityUsed();
            }
        }
    }
    public List<Character> GetTargets()
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemy)
        {
            foreach(Character e in gameManager.activeEnemies.Where(e => e.targetable = true))
            {
                targets.Add(e);
            }
        }
        if (targetsAlly)
        {
            foreach(Character h in gameManager.activeHeroes)
            {
                targets.Add(h);
            }
        }
        return targets;
    }
}

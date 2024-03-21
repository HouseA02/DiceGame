using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFace : Effect
{
    [SerializeField]
    private Ability ability;

    public override void Activate(Character source, Character target, float value)
    {
        base.Activate(source, target);
        List<Ability> validFaces = new List<Ability>();
        foreach(Ability targetAbility in target.abilities)
        {
            if((targetAbility.abilityName != ability.abilityName) && (targetAbility.abilityName != "Blank"))
            {
                validFaces.Add(targetAbility);
            }
        }
        if(validFaces.Count > 0)
        {
            int targetID = validFaces[Random.Range(0, validFaces.Count)].id;
            target.AddAbility(ability, targetID);
        }
    }

    public override void Activate(Character target)
    {
        base.Activate(target);
        target.AddAbility(ability, Random.Range(0, target.abilities.Length));
    }
}

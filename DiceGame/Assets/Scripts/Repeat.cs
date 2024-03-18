using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Repeat : Effect
{
    [SerializeField] 
    private List<AbilityEffect> effects;
    [SerializeField]
    private float delay = 0.2f;
    public override void Activate(Character source, Character target, float value)
    {
        StartCoroutine(LoopEffects(source, target, value));
        base.Activate(source, target, value);
    }

    IEnumerator LoopEffects(Character source, Character target, float value)
    {
        for (int i = 0; i < value; i++)
        {
            effects.ForEach(e => e.Activate(source, target));
            yield return new WaitForSeconds(delay);
        }
    }
}

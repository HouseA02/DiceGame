using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRoll : Effect
{
    [SerializeField]
    public Effect[] effects;
    [SerializeField]
    public Die dieToRoll;

    public override void Activate(Character source, Character target, float value)
    {
        StartCoroutine(RollDie(source, target, value));
    }

    public virtual IEnumerator RollDie(Character source, Character target, float value)
    {
        Die dieInstance = Instantiate(dieToRoll);
        dieInstance.transform.position = source.dieReference.transform.position;
        dieInstance.Roll();
        yield return new WaitUntil(dieInstance.rb.IsSleeping);
        if (effects[dieInstance.value-1] != null)
        {
            effects[dieInstance.value-1].Activate(source, target, value);
        }
        Destroy(dieInstance.gameObject);
    }
}

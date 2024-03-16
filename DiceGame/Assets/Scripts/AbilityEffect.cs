using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class AbilityEffect
{
    public Effect effect;
    public float value;
    public void Activate(Character source, Character target)
    {
        effect.Activate(source, target, value);
    }

    public void Activate(Character source, List<Character> targets)
    {
        effect.Activate(source, targets, value);
    }

    public virtual void Activate()
    {
        effect.Activate(value);
    }

    public virtual void Activate(Character target)
    {
        effect.Activate(target, value);
    }
}
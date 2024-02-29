using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDie : Die
{
    public Ability[] abilities;
    public Character characterReference;
    public override void Activate(int value)
    {
        if (abilities[value-1] != null)
        {
            characterReference.SetAbility(abilities[value-1]);
        }
    }
}

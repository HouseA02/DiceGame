using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityDie : Die
{
    public AbilityFace[] abilities;

    public override void Activate(int value)
    {
        if (abilities[value-1] != null)
        {
            abilities[value-1].Activate();
        }
    }
}

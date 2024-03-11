using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHeal : Heal
{
    [SerializeField]
    private int value;
    public override void Activate(Character target)
    {
        target.ChangeHP(value);
    }
}


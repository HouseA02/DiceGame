using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAttack : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        source.enemies.ForEach(e  => { e.TakeDamage((int)(value * source.power)); });
    }
}

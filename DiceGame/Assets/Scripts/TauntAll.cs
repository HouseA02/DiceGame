using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntAll : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        foreach (Enemy enemy in source.enemies)
        {
            if (source.allies.Contains(enemy.target))
            {
                enemy.UpdateTarget(source);
            }
        }
    }
}

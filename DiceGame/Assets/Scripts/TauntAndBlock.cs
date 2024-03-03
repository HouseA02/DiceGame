using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntAndBlock : SelfBuff
{
    public override void Activate()
    {
        base.Activate();
        foreach (Enemy enemy in characterReference.enemies)
        {
            if(characterReference.allies.Contains(enemy.target)) 
            {
                enemy.UpdateTarget(characterReference);
            }
        }
        characterReference.CleanUp();
        characterReference.OnAbilityUsed();
    }
}


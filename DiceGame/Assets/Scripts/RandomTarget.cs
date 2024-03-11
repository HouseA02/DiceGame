using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

public class RandomTarget : Effect
{
    [SerializeField]
    Effect effect;
    public bool targetsEnemies;
    public bool targetsAllies;
    public bool targetsSelf;

    public override void Activate(Character source, Character target, float value)
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(source.enemies); }
        if (targetsAllies) { targets.AddRange(source.allies); }
        if (targetsSelf) { targets.Add(source); }
        if (targets.Count > 0)
        {
            Character randTarget = targets[Random.Range(0, targets.Count)];
            effect.Activate(source, randTarget, value);
        }
    }

    public override void Activate(float value)
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(gameManager.activeEnemies); }
        if (targetsAllies) { targets.AddRange(gameManager.activeHeroes); }
        if (targets.Count > 0)
        {
            Character randTarget = targets[Random.Range(0, targets.Count)];
            effect.Activate(randTarget, value);
        }
    }

    public override void Activate()
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(gameManager.activeEnemies); }
        if (targetsAllies) { targets.AddRange(gameManager.activeHeroes); }
        if (targets.Count > 0)
        {
            Character randTarget = targets[Random.Range(0, targets.Count)];
            effect.Activate(randTarget);
        }
    }
}
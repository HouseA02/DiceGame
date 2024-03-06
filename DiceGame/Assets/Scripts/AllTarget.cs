using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AllTarget : Effect
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
        if (targetsAllies) {  targets.AddRange(source.allies); }
        if (targetsSelf) { targets.Add(source); }
        targets.ForEach(t => effect.Activate(source, t, value));
    }

    public override void Activate(float value)
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(gameManager.activeEnemies); }
        if (targetsAllies) { targets.AddRange(gameManager.activeHeroes); }
        Debug.Log("List Count" + targets.Count);
        targets.ForEach(t => effect.Activate(t, value));
    }
}

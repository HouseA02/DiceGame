using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.Rendering.DebugUI;

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
        if (VFX != null)
        {
            effect.VFX = VFX;
        }
        targets.ForEach(t => effect.Activate(source, t, value));
    }

    public override void Activate(float value)
    {
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(gameManager.activeEnemies); }
        if (targetsAllies) { targets.AddRange(gameManager.activeHeroes); }
        Debug.Log("List Count" + targets.Count);
        if (VFX != null)
        {
            effect.VFX = VFX;
        }
        targets.ForEach(t => effect.Activate(t, value));
    }

    public override void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        List<Character> targets = new List<Character>();
        if (targetsEnemies) { targets.AddRange(gameManager.activeEnemies); }
        if (targetsAllies) { targets.AddRange(gameManager.activeHeroes); }
        if (VFX != null)
        {
            effect.VFX = VFX;
        }
        targets.ForEach(t => effect.Activate(t));
    }
}

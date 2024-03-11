using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : Effect
{
    [SerializeField]
    private CombatData combatData;
    public override void Activate()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        gameManager.StartCombat(combatData);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyEntry : CompendiumEntry
{
    [SerializeField]
    private Enemy enemy;
    [SerializeField]
    private Image portraitSlot;
    [SerializeField]
    private Image portraitSlot2;
    [SerializeField]
    private DieSpreadImage[] abilitySlots;
    
    public void Initialise(Enemy enemy)
    {
        portraitSlot.sprite = enemy.portrait;
        portraitSlot2.sprite = enemy.portrait;
        this.enemy = enemy;
        for(int i = 0; i < abilitySlots.Length; i++)
        {
            abilitySlots[i].Initialise(enemy.baseAbilities[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CombatEvent : Event
{
    [SerializeField]
    public CombatData combatData;
    public Image[] imageHolders;
    Vector2[] defaultPositions = new Vector2[3];
    [SerializeField]
    private GameObject enemyPanel;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!isBlinded){ enemyPanel.SetActive(true); }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (!isBlinded) { enemyPanel.SetActive(false); }
    }
    public void Initialise(CombatData newCombatData)
    {
        combatData = newCombatData;
        for (int i = 0; i < imageHolders.Length; i++)
        {
            defaultPositions[i] = imageHolders[i].transform.localPosition;
        }
        for (int i = 0; i < imageHolders.Length; i++)
        {
            if(combatData.type != CombatData.CombatType.Boss)
            {
                imageHolders[i].sprite = combatData.enemies[i].portrait;
                imageHolders[i].transform.localScale = Vector3.one * (1 + combatData.enemies[i].spriteSize);
                imageHolders[i].transform.localPosition = defaultPositions[i] + combatData.enemies[i].spriteOffset / 150;
            }
        }
    }

    public void InitialiseRand(List<Enemy> validEnemies)
    {
        combatData = (CombatData)ScriptableObject.CreateInstance("CombatData");
        List<Character> randEnemies = new List<Character>();
        for(int i = 0; i < 3; i++)
        {
            randEnemies.Add(validEnemies[Random.Range(0, validEnemies.Count)]);
        }
        combatData.type = CombatData.CombatType.Normal;
        combatData.enemies = randEnemies;
        for (int i = 0; i < imageHolders.Length; i++)
        {
            defaultPositions[i] = imageHolders[i].transform.localPosition;
        }
        for (int i = 0; i < imageHolders.Length; i++)
        {
            if (combatData.type != CombatData.CombatType.Boss)
            {
                imageHolders[i].sprite = combatData.enemies[i].portrait;
                imageHolders[i].transform.localScale = Vector3.one * (1 + combatData.enemies[i].spriteSize);
                imageHolders[i].transform.localPosition = defaultPositions[i] + combatData.enemies[i].spriteOffset / 150;
            }
        }
    }
}

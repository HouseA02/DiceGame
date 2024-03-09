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
        enemyPanel.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        enemyPanel.SetActive(false);
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
            imageHolders[i].sprite = combatData.enemies[i].portrait;
            imageHolders[i].transform.localScale = Vector3.one * (1 + combatData.enemies[i].spriteSize);
            imageHolders[i].transform.localPosition = defaultPositions[i] + combatData.enemies[i].spriteOffset/150;
        }
    }
}

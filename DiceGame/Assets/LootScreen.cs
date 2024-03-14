using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LootScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private TMP_Text[] buttonText;
    [SerializeField]
    private Image[] buttonImage;
    [SerializeField]
    private LootManager lootManager;

    public void FindLoot(CombatData combatData)
    {
        buttons[0].gameObject.SetActive(true);
        switch (combatData.type)
        {
            case CombatData.CombatType.Normal:
                buttonText[0].text = Random.Range(lootManager.loot[0].soulsMin, lootManager.loot[0].soulsMax).ToString() + " Souls";
                break;
            case CombatData.CombatType.Elite:
                buttonText[0].text = Random.Range(lootManager.loot[1].soulsMin, lootManager.loot[1].soulsMax).ToString() + " Souls";
                break;
            case CombatData.CombatType.Boss:
                buttonText[0].text = Random.Range(lootManager.loot[2].soulsMin, lootManager.loot[2].soulsMax).ToString() + " Souls";
                break;
            default:
                break;
        }
    }

}

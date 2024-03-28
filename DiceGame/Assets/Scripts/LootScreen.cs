using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class LootScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject addFacePanel;
    [SerializeField]
    private Image[] addFacePanelFaces;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Player player;
    [SerializeField]
    public GameObject panel;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private TMP_Text[] buttonText;
    [SerializeField]
    private Image[] buttonImage;
    [SerializeField]
    private LootManager lootManager;
    private int soulsToGet;
    private Ability faceToLoot;
    private Hero validHero = null;
    public void FindLoot(CombatData combatData)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(PlaySound);
        }
        buttons[0].gameObject.SetActive(true);
        switch (combatData.type)
        {
            case CombatData.CombatType.Normal:
                soulsToGet = Random.Range(lootManager.soulBounty[0].soulsMin, lootManager.soulBounty[0].soulsMax);
                buttonText[0].text = soulsToGet.ToString() + " Souls";
                break;
            case CombatData.CombatType.Elite:
                soulsToGet = Random.Range(lootManager.soulBounty[1].soulsMin, lootManager.soulBounty[1].soulsMax);
                buttonText[0].text = soulsToGet.ToString() + " Souls";
                break;
            case CombatData.CombatType.Boss:
                soulsToGet = Random.Range(lootManager.soulBounty[2].soulsMin, lootManager.soulBounty[2].soulsMax);
                buttonText[0].text = soulsToGet.ToString() + " Souls";
                break;
            default:
                break;
        }
        buttons[0].onClick.AddListener(ClaimSouls);
        buttons[1].gameObject.SetActive(true);
        faceToLoot = lootManager.facePool[Random.Range(0, lootManager.facePool.Count)];
        buttonImage[1].sprite = faceToLoot.UIImage;
        buttonText[1].text = faceToLoot.name;
        buttons[1].onClick.AddListener(ClaimFace);
    }

    void PlaySound()
    {
        lootManager.audioSource.Play();
    }
    void ClaimSouls()
    {
        player.ChangeSouls(soulsToGet);
        soulsToGet = 0;
        buttons[0].onClick.RemoveAllListeners();
        buttons[0].gameObject.SetActive(false);
    }
    void ClaimFace()
    {
        foreach (Hero hero in gameManager.activeHeroes)
        {
            if (hero.m_Class.Contains(faceToLoot.pool))
            {
                validHero = hero;
                break;
            }
        }
        addFacePanel.gameObject.SetActive(true);
        for(int i = 0; i < addFacePanelFaces.Length; i++)
        {
            addFacePanelFaces[i].sprite = validHero.abilities[i].UIImage;
        }
    }

    public void ClaimFace(Ability rewardFace)
    {
        faceToLoot = rewardFace;
        foreach (Hero hero in gameManager.activeHeroes)
        {
            if (hero.m_Class.Contains(faceToLoot.pool))
            {
                validHero = hero;
                break;
            }
        }
        addFacePanel.gameObject.SetActive(true);
        for (int i = 0; i < addFacePanelFaces.Length; i++)
        {
            addFacePanelFaces[i].sprite = validHero.abilities[i].UIImage;
        }
    }

    public void AddFace(int faceTarget)
    {
        if (validHero.abilities[faceTarget].isReplacable)
        {
            validHero.AddAbility(faceToLoot, faceTarget);
            validHero = null;
            faceToLoot = null;
            addFacePanel.SetActive(false);
            buttons[1].onClick.RemoveAllListeners();
            buttons[1].gameObject.SetActive(false);
        }
    }
}

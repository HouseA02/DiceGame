using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Image cutout;
    [SerializeField]
    private Image Profile;
    [SerializeField]
    private Image Health;
    [SerializeField]
    private Image Rerolls;
    [SerializeField]
    private Image EndTurn;
    [SerializeField]
    private Image Result;
    [SerializeField]
    private Image Target;
    [SerializeField]
    private Image StatusEffectsImage;
    [SerializeField]
    private GameObject StatusEffects;
    [SerializeField]
    private GameObject MapTutorialObject;

    delegate void FocusFunctions();
    private List<FocusFunctions> funcList = new List<FocusFunctions>();

    private int i = 0;
    public bool canRun;
    public bool isMap = true;
    void Start()
    {
        funcList.Add(BattleTutorial);
        funcList.Add(FocusProfile);
        funcList.Add(FocusHealth);
        funcList.Add(FocusResultContainer1);
        funcList.Add(FocusResultContainer2);
        funcList.Add(FocusStatusEffects);
        //funcList.Add(FocusTargetContainer);
        funcList.Add(FocusRerolls);
        funcList.Add(FocusEndTurn);
    }

    public void sceneCheck()
    {
        if (isMap)
        {
            MapTutorial();
        }
        else
        {
            BattleTutorial();
        }
    }

    void MapTutorial()
    {
        MapTutorialObject.SetActive(true);
    }

    public void CloseMapTutorial()
    {
        MapTutorialObject.SetActive(false);
    }

    void BattleTutorial()
    {
        canRun = true;
        cutout.gameObject.SetActive(true);
        text.SetText("Time to die is a turn-based rougelike wherein your choices for each turn of gameplay are decided by the roll of a dice");
        text.gameObject.SetActive(true);
    }

    void FocusProfile()
    {
        cutout.gameObject.SetActive(true);
        cutout.rectTransform.sizeDelta = Profile.rectTransform.sizeDelta;
        cutout.transform.localScale = Profile.transform.localScale;
        cutout.transform.position = Profile.transform.position;
        text.SetText("Each character has a viewable profile. Clicking the icon will show what each side of your dice will do");
    }

    void FocusHealth()
    {
        cutout.rectTransform.sizeDelta = Health.rectTransform.sizeDelta;
        cutout.transform.localScale = Health.transform.localScale;
        cutout.transform.position = Health.transform.position;
        text.SetText("Characters health is represented through the dice icon at the corner of their profile\r\n");
    }

    void FocusRerolls()
    {
        cutout.rectTransform.sizeDelta = Rerolls.rectTransform.sizeDelta;
        cutout.transform.localScale = Rerolls.transform.localScale;
        cutout.transform.position = Rerolls.transform.position;
        text.SetText("Use right mouse click to reroll your dice. Rerolling gives the chance to land a more favourable face");
    } 

    void FocusEndTurn()
    {
        cutout.rectTransform.sizeDelta = EndTurn.rectTransform.sizeDelta;
        cutout.transform.localScale = EndTurn.transform.localScale;
        cutout.transform.position = EndTurn.transform.position;
        text.SetText("Once you've used all of your skills, press End Turn to move to the next phase of battle");
    }

    /*void FocusTargetContainer()
    {
        cutout.rectTransform.sizeDelta = Target.rectTransform.sizeDelta;
        cutout.transform.localScale = Target.transform.localScale;
        cutout.transform.position = Target.transform.position;
        text.SetText("Characters being targeted are shown next to the enemy about to attack");
    }*/

    void FocusResultContainer1()
    {
        cutout.rectTransform.sizeDelta = Result.rectTransform.sizeDelta;
        cutout.transform.localScale = Result.transform.localScale;
        cutout.transform.position = Result.transform.position;
        text.SetText("Results of each dice roll are shown next to their respective character. Clicking the icon will initiate the skill in question");
    }
    void FocusResultContainer2()
    {
        text.SetText("Some abilities will activate as you click them, others may require you to choose a target. Viable targets will glow red");
    }

    void FocusStatusEffects()
    {
        StatusEffects.SetActive(true);
        cutout.rectTransform.sizeDelta = StatusEffectsImage.rectTransform.sizeDelta;
        cutout.transform.localScale = StatusEffectsImage.transform.localScale;
        cutout.transform.position = StatusEffectsImage.transform.position;
        text.SetText("Abilities may also bestow buffs or debuffs onto characters, indicated at the side of their profile. Hovering over effect icons will detail them");
    }

    void FocusRelic()
    {
        /*
        cutout.rectTransform.sizeDelta = Result.rectTransform.sizeDelta;
        cutout.transform.localScale = Result.transform.localScale;
        cutout.transform.position = Result.transform.position;
        text.SetText("Some encounters will grant you powerful items known as relics, which can upgrade stats, abilites, or even change the faces of your die");
        */
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canRun)
        {
            i++;
            if (i < funcList.Count)
            {
                StatusEffects.SetActive(false);
                funcList[i]();
            }
            else
            {
                canRun = false;
                cutout.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
                i = 0;
            }
        }
    }
}

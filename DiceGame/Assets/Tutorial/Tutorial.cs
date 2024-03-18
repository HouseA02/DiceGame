using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public bool isComplete;
    public bool mapTutorialComplete;

    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private TextAsset textFile;
    [SerializeField]
    private GameObject CharacterPanels;
    [SerializeField]
    private GameObject EnemyPanels;
    [SerializeField]
    private GameObject RerollPanel;
    [SerializeField]
    private GameObject EndTurnPanel;
    [SerializeField]
    private GameObject RelicPanel;
    [SerializeField]
    private TMP_Text textBox;
    private Image textBoxParent;
    private Animator textBoxAnim;
    [SerializeField]
    private GameObject textBoxButton;
    [SerializeField]
    private GameObject mapFuncButton;
    [SerializeField]
    private RectTransform[] quads;

    [SerializeField]
    private GameManager gameManager;

    private List<string> dialogues = new List<string>();
    private delegate void CombatFunctionDelegate();
    private delegate void MapFunctionDelagate();
    private List<CombatFunctionDelegate> combatFunctions = new List<CombatFunctionDelegate>();
    private List<MapFunctionDelagate> mapFunctions = new List<MapFunctionDelagate>();

    public List<GameObject> dice = new List<GameObject>();

    private int currentLine;
    public int currentFunc;
    private bool continueDialogue;
    void Start()
    {
        PopulateDialogues();
        PopulateCombatFunctions();
        PopulateMapFunctions();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && continueDialogue)
        {
            currentLine++;
            if(currentLine < dialogues.Count) 
            {
                dialogueManager.Initiate(dialogues[currentLine]);
            }
            else
            {
                dialogueManager.EndDialogue();
                continueDialogue = false;
                currentLine = 0;
                textBoxParent = textBox.gameObject.transform.parent.gameObject.GetComponent<Image>();
                textBoxParent.gameObject.SetActive(true);
            }
        }
    }

    void PopulateDialogues()
    {
        string[] speakers = textFile.text.Split('\n');
        foreach(string line in speakers)
        {
            if(line.Length > 1)
            {
                dialogues.Add(line);
            }
            //replace this if statement with something that just skips empty lines
        }
    }

    void PopulateCombatFunctions()
    {
        combatFunctions.Add(Func1);
        combatFunctions.Add(Func2);
        combatFunctions.Add(Func3);
        combatFunctions.Add(Func4);
        combatFunctions.Add(Func5);
        combatFunctions.Add(Func6);
        //combatFunctions.Add(Func7);
        combatFunctions.Add(endFunc);
    }
    void PopulateMapFunctions()
    {
        mapFunctions.Add(MapFunc1);
        mapFunctions.Add(MapFunc2);
        mapFunctions.Add(MapFunc3);
        mapFunctions.Add(MapFunc4);
        mapFunctions.Add(MapFunc5);
        mapFunctions.Add(MapFunc6);
        mapFunctions.Add(MapFunc7);
        mapFunctions.Add(MapFuncEnd);
    }

    public void nextLine()
    {
        continueDialogue = true;
        dialogueManager.Initiate(dialogues[currentLine]);
        CharacterPanels.SetActive(false);
        EnemyPanels.SetActive(false);
        RerollPanel.SetActive(false);
        EndTurnPanel.SetActive(false);
        RelicPanel.SetActive(false);
    }

    public void nextFunc()
    {
        combatFunctions[currentFunc]();
        currentFunc++;
    }
    public void nextMapFunc()
    {
        mapFunctions[currentFunc]();
        currentFunc++;
    }

    public void preventCombat()
    {
        isComplete = true;
    }

    void Func1()
    {
        textBox.text = "All combatants possess at least one dice, which when rolled will offer that combatant an ability, determined by the upwards face";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        gameManager.StartTurn();
        foreach (GameObject dice in dice)
        {
            var diceScript = dice.GetComponent<AbilityDie>();
            var charRef = diceScript.characterReference;
            Transform dicePos = charRef.transform.Find("DiceSpawnPos").transform;
            dice.transform.position = dicePos.position;
        }
        textBoxAnim = textBoxParent.GetComponent<Animator>();
        textBoxAnim.SetTrigger("Animate");
    }
    void Func2()
    {
        textBox.text = "Abilites can be used by clicking the identical icon that appears next to each combatants profile.";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        textBoxAnim.SetTrigger("Animate");
        for(int i = 0; i < CharacterPanels.transform.childCount;  i++)
        {
            CharacterPanels.transform.GetChild(i).gameObject.SetActive(false);
        }
        CharacterPanels.SetActive(true);
        GameObject firstPanel = CharacterPanels.transform.GetChild(0).gameObject;
        firstPanel.SetActive(true);
    }
    void Func3()
    {
        textBox.text = "Use the Rogue's primary attack on Timecaster.";
        textBoxParent.rectTransform.localPosition = quads[2].localPosition;
        textBoxAnim.SetTrigger("Animate");
        textBoxButton.SetActive(false);
    }
    void Func4()
    {
        textBox.text = "Offensive abilities sometimes require a specified target, indicated by a red glow beneath any valid targets.";
        textBoxParent.rectTransform.localPosition = quads[2].localPosition;
        textBoxAnim.SetTrigger("Animate");
        EnemyPanels.SetActive(true);
    }
    void Func5()
    {
        textBox.text = "Combatants health is indicated by the red dice at the corner of their profile. Once it reaches zero, that combatant can no longer fight.";
        textBoxParent.rectTransform.localPosition = quads[2].localPosition;
        textBoxAnim.SetTrigger("Animate");
        textBoxButton.SetActive(true);   
    }
    void Func6()
    {
        textBox.text = "Not all abilities are offensive. The Knight character can use a number of defensive abilities to prevent incoming damage.";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        textBoxAnim.SetTrigger("Animate");
        GameObject secondPanel = CharacterPanels.transform.GetChild(1).gameObject;
        secondPanel.SetActive(true);
    }
    void Func7()
    {
        textBox.text = "Use the Knights Block ability on the rogue.";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        textBoxAnim.SetTrigger("Animate");
        textBoxButton.SetActive(false);
    }
    void endFunc()
    {
        textBoxParent.gameObject.SetActive(false);
        CharacterPanels.transform.GetChild(2).gameObject.SetActive(true);
        EnemyPanels.SetActive(true);
        RerollPanel.SetActive(true);
        EndTurnPanel.SetActive(true);
        RelicPanel.SetActive(true);
        isComplete = false;
    }


    void MapFunc1()
    {
        textBoxParent.gameObject.SetActive(true);
        textBox.text = "This is the map view. Your objective is to travel around the clock face, choosing which path you wish to take.";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        mapFuncButton.SetActive(true);
    }
    void MapFunc2()
    {
        textBox.text = "Every hour of the clock presents 3 choices, of which you may only pick 1 before the clock hand moves forward";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFunc3()
    {
        textBox.text = "Each choice you make will be one of 4 options: Normal Enemy Encounter, \nElite Enemy Encounters, \nStory Encounters, \nOr Rest Encounters";
        textBoxParent.rectTransform.localPosition = quads[0].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFunc4()
    {
        textBox.text = "Normal and Elite Enemy Encounters. Normal enemies bare little challenge and little reward, whereas Elite enemies will provide a greater challenge, and a greater reward.";
        textBoxParent.rectTransform.localPosition = quads[0].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFunc5()
    {
        textBox.text = "Story Encounters may grant helpful reward items for future combat, or they may throw you into combat themselves. Are the rewards worth the chance?";
        textBoxParent.rectTransform.localPosition = quads[1].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFunc6()
    {
        textBox.text = "Rest Encounters present neither combat nor items, but will allow your team to recover some health before the next battle.";
        textBoxParent.rectTransform.localPosition = quads[3].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFunc7()
    {
        textBox.text = "Traversing the entire clock face presents the honor to battle the strongest opponent at the centre of the map, an opponent stronger than all the rest.";
        textBoxParent.rectTransform.localPosition = quads[2].localPosition;
        textBoxAnim.SetTrigger("Animate");
    }
    void MapFuncEnd()
    {
        textBoxParent.gameObject.SetActive(false);
        isComplete = false;
        mapTutorialComplete = true;
    }
}

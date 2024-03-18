using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public bool isComplete;

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
    private RectTransform[] quads;

    [SerializeField]
    private GameManager gameManager;

    private List<string> dialogues = new List<string>();
    private delegate void FunctionDelegate();
    private List<FunctionDelegate> tutorialFunctions = new List<FunctionDelegate>();

    public List<GameObject> dice = new List<GameObject>();

    private int currentLine;
    public int currentFunc;
    private bool continueDialogue;
    void Start()
    {
        PopulateDialogues();
        PopulateFuncList();
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

    void PopulateFuncList()
    {
        tutorialFunctions.Add(Func1);
        tutorialFunctions.Add(Func2);
        tutorialFunctions.Add(Func3);
        tutorialFunctions.Add(Func4);
        tutorialFunctions.Add(Func5);
        tutorialFunctions.Add(Func6);
        //tutorialFunctions.Add(Func7);
        tutorialFunctions.Add(endFunc);
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
        tutorialFunctions[currentFunc]();
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
    }
}

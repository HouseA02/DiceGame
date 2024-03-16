using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public bool isComplete;

    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private TextAsset textFile;

    [SerializeField]
    private List<string> dialogues = new List<string>();

    public int currentLine;
    private bool continueDialogue;
    void Start()
    {
        PopulateDialogues();
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

    public void nextLine()
    {
        continueDialogue = true;
        dialogueManager.Initiate(dialogues[currentLine]);
    }
}

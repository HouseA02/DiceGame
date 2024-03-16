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
    private List<string> dialogues = new List<string>();

    int currentLine;
    void Start()
    {
        PopulateDialogues();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueManager.Initiate(dialogues[currentLine]);
            currentLine++;
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
}

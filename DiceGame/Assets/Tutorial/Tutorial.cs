using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public bool isComplete;
    public DialogueManager dialogueManager;

    private GameObject speaker;
    private string text;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dialogueManager.Initiate(speaker, text);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameBox;
    [SerializeField]
    private TMP_Text textBox;
    [SerializeField]
    private Transform battleArenaCam;

    private string speaker, text;
    private GameObject speakerObject;
    private GameObject textParent;

    public void Initiate(string line)
    {
        var half = line.Split(':');
        speaker = half[0];
        text = half[1];
        speakerObject = GameObject.Find(speaker);
        textParent = textBox.transform.parent.gameObject;
        textParent.SetActive(true);
        textParent.transform.LookAt(battleArenaCam);
        textBox.text = text;
        nameBox.text = speaker;
    }

    public void EndDialogue()
    {
        textParent.SetActive(false);
    }
}

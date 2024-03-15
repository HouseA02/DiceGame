using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    private string speaker, text;
    private GameObject speakerObject;

    public void Initiate(string line)
    {
        var half = line.Split(':');
        speaker = half[0];
        text = half[1];
        speakerObject = GameObject.Find(speaker);
        cube.transform.position = speakerObject.transform.position;
        print(text);
    }
}

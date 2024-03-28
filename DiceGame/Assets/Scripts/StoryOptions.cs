using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryOptions : MonoBehaviour
{
    [SerializeField]
    public Option[] Options;
    private void Awake()
    {
        Options = GetComponentsInChildren<Option>();
    }

    private void Reset()
    {
        Option[] optionsToAdd = GetComponentsInChildren<Option>();
        Array.Reverse(optionsToAdd);
        Options = optionsToAdd;
    }
}

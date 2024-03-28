using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Option : MonoBehaviour
{
    public string text;
    public List<Effect> effects = new List<Effect>();
    public void Activate()
    {
        effects.ForEach(effect => effect.Activate());
    }
}

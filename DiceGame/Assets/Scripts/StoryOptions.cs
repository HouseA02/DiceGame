using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryOptions : MonoBehaviour
{
    [Serializable]
    public class Option
    {
        public string text;
        public List<Effect> effects= new List<Effect>();

        public void Activate()
        {
            effects.ForEach(effect => effect.Activate());
        }
    }
    [SerializeField]
    public Option[] Options;
}

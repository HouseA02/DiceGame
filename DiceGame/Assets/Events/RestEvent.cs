using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestEvent : StoryEvent
{
    private void Awake()
    {
        eventType = EventType.Rest; 
    }
}


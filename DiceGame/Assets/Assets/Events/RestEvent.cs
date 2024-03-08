using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestEvent : Event
{
    private void Awake()
    {
        eventType = EventType.Rest; 
    }
}


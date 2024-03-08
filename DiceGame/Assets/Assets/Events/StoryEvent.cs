using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : Event
{
    private void Awake()
    {
        eventType = EventType.Story;
    }
}

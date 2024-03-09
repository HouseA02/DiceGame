using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : Event
{
    public StoryData storyData;
    private void Awake()
    {
        eventType = EventType.Story;
    }
}

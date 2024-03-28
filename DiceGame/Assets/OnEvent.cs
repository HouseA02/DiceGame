using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnEvent : Relic
{
    EventArenaController eventArenaController;
    public UnityEvent r_onRest = new UnityEvent();
    public UnityEvent r_onCombat = new UnityEvent();
    public UnityEvent r_onElite = new UnityEvent();
    public UnityEvent r_onStory = new UnityEvent();
    public override void Initialise(GameManager gm)
    {
        base.Initialise(gm);
        eventArenaController = gameManager.eventArenaController;
        eventArenaController.m_mapEvent.AddListener(OnEventTrigger);
    }

    void OnEventTrigger(Event e)
    {
        switch(e.eventType)
        {
            case Event.EventType.Rest:
                r_onRest.Invoke();
                break;
            case Event.EventType.Combat:
                r_onCombat.Invoke();
                break;
            case Event.EventType.Elite:
                r_onElite.Invoke();
                break;
            case Event.EventType.Story:
                r_onStory.Invoke();
                break;
        }
    }
    
}

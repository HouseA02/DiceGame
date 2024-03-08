using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent : Event
{
    [SerializeField]
    public CombatData combatData;

    private void Awake()
    {
        eventType = EventType.Combat;
    }
}

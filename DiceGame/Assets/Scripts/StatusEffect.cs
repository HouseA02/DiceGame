using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StatusEffect : MonoBehaviour
{
    public string statusName;
    public Sprite sprite;
    public StatusSlot slot;
    public int value;
    public Character characterReference;

    public virtual void Initialise(Character character, int newValue)
    {
        characterReference = character;
        value = newValue;
        transform.parent = characterReference.transform;
        characterReference.m_OnTurnStart.AddListener(OnTurnStart);
        characterReference.m_OnTurnEnd.AddListener(OnTurnEnd);
        OnApplied();
    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnApplied()
    {

    }
    public virtual void AddValue(int addValue)
    {
        value += addValue;
        slot.UpdateValue(value);
    }
    public virtual void Expire()
    {
        characterReference.statusEffects.Remove(this);
        Destroy(gameObject);
    }
}

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
    public string description;
    public Color valueColor;
    public TriggerEffect triggerEffect;

    public virtual void Initialise(Character character, int newValue)
    {
        characterReference = character;
        value = newValue;
        transform.parent = characterReference.transform;
        characterReference.m_OnTurnStart.AddListener(OnTurnStart);
        characterReference.m_OnTurnEnd.AddListener(OnTurnEnd);
    }

    public virtual void OnTurnStart()
    {

    }

    public virtual void OnTurnEnd()
    {

    }

    public virtual void OnApplied()
    {
        TriggerEffect();
    }

    public virtual void TriggerEffect()
    {
        if (triggerEffect != null)
        {
            var effectInstance = Instantiate(triggerEffect, slot.transform);
            effectInstance.image.sprite = sprite;
        }
    }
    public virtual void AddValue(int addValue)
    {
        value += addValue;
        UpdateValue();
    }

    public virtual void OnExpire()
    {

    }

    public virtual void UpdateValue()
    {
        slot.UpdateValue(value);
        if (value <= 0) { Expire(); }
    }
    public virtual void Expire()
    {
        OnExpire();
        slot.Expire();
        characterReference.statusEffects.Remove(this);
        Destroy(gameObject);
    }
}

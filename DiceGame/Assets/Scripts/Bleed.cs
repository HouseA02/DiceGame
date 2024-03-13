using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : StatusEffect
{
    public override void Initialise(Character character, int newValue)
    {
        character.m_OnHeal.AddListener(OnHeal);
        base.Initialise(character, newValue);
    }
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        characterReference.ChangeHP(-value);
        value--;
        UpdateValue();
    }

    public void OnHeal(int heal)
    {
        Expire();
    }
}

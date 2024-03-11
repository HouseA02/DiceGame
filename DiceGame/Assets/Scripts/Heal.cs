using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Effect
{
    int healDecay;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        gameManager.gm_OnBattleStart.AddListener(ResetDecay);
    }

    private void ResetDecay()
    {
        healDecay = 0;
    }
    public override void Activate(Character source, Character target, float value)
    {
        int newValue = (int)value-healDecay;
        newValue = Mathf.Clamp(newValue, 0, 999);
        target.ChangeHP(newValue);
        healDecay++;
        base.Activate(source, target, value);
    }
}

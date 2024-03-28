using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddReroll : Effect
{
    public override void Activate(float value)
    {
        Debug.Log("aa");
        gameManager.rerolls += (int)value;
        gameManager.rerollText.text = new string($"Rerolls: {gameManager.rerolls}");
    }
}

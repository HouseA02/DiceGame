using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfBuff : Ability
{
    [SerializeField]
    int blockAmount;

    public override void Activate()
    {
        base.Activate();
        characterReference.ChangeBlock(blockAmount, true);
    }
}

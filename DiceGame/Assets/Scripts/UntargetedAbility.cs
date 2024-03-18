using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntargetedAbility : Ability
{
    public override void Activate()
    {
        base.Activate();
        StartCoroutine(UseAbility());
    }
}

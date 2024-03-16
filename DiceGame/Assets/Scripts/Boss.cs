using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Boss : Enemy
{
    public override void Roll()
    {
        base.Roll();
        dieReference.transform.localScale *= 2;
        foreach(DecalProjector decal in dieReference.decals) 
        {
            decal.size *= 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBlock : Effect
{
    public override void Activate(Character source, Character target, float value)
    {
        source.allies.ForEach(a => { a.ChangeBlock((int)value,false); });
        source.ChangeBlock((int)value,false);
    }

}

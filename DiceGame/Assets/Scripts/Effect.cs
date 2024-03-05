using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public virtual void Activate(Character source, Character target, float value)
    {

    }

    public virtual void Activate(Character source, List<Character> targets, float value)
    {

    }
}

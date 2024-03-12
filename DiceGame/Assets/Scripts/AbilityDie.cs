using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class AbilityDie : Die
{
    public Character characterReference;
    public Ability landedAbility;
    public DecalProjector[] decals;
    public override void Activate(int value)
    {
        characterReference.SetAbility(value - 1);
    }

    void ChangeAllDecals(Ability[] abilities)
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            Debug.Log(i);
            if (abilities[i] != null)
            {
                decals[i].material = abilities[i].image;
            }
        }
    }
    public void Initialise(Character character)
    {
        characterReference = character;
        ChangeAllDecals(character.abilities);
        GetComponent<Renderer>().material.color = characterReference.mainColor;
    }
}

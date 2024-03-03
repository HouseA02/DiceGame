using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    public int HP;
    public TMP_Text HPText;
    public Image resultImage;
    public RawImage portraitSlot;
    public Vector3 defaultPosition;
    public Character character;
    public Image background;

    public void Initialise(Character newCharacter)
    {
        defaultPosition = portraitSlot.transform.position;
        character = newCharacter;
        HPText.text = character.HP.ToString();
        if (character.portrait != null) { 
            portraitSlot.texture = character.portrait;
            portraitSlot.transform.Translate(character.spriteOffset);
        }
        background.color = character.mainColor;
    }

    public void SetHP(int value)
    {
        HPText.text = value.ToString();
    }
    public void UseAbility()
    {
        if (character.currentAbility != null)
        {
            character.PrimeAbility();
        }
    }

}

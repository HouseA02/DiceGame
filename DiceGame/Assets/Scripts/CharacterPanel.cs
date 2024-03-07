using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    public int HP;
    public TMP_Text nameText;
    public TMP_Text HPText;
    public TMP_Text blockText;
    public GameObject blockContainer;
    public Image resultImage;
    public Image portraitSlot;
    public Vector3 defaultPosition;
    public Character character;
    public Image background;
    public Target targetSprite;
    public DieSpreadImage[] dieSpreadImages;
    ///
    public GameObject descContainer;
    public TMP_Text descriptionText;
    public TMP_Text abilityNameText;
    ///
    public GameObject targetContainer;
    public Image targetImage;
    /// 
    public StatusSlot[] statusSlots;

    public void Initialise(Character newCharacter)
    {
        defaultPosition = portraitSlot.transform.position;
        character = newCharacter;
        nameText.text = character.characterName;
        HPText.text = character.HP.ToString();
        if (character.portrait != null) { 
            portraitSlot.sprite = character.portrait;
            portraitSlot.transform.localScale = Vector3.one * (1+character.spriteSize);
            portraitSlot.transform.Translate(character.spriteOffset);
        }
        background.color = character.mainColor;
        for (int i = 0; i < character.abilities.Length; i++) 
        {
            dieSpreadImages[i].Initialise(character.abilities[i]);
        }
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

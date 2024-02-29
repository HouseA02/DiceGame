using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    AbilityDie die;
    [SerializeField]
    public Vector2 spriteOffset;
    [SerializeField]
    public Color mainColor;
    [SerializeField]
    public int maxHP;
    public int HP;
    public Texture portrait;
    public CharacterPanel characterPanel;
    
    
    public void Roll()
    {
        AbilityDie dieInstance = Instantiate(die);
        dieInstance.characterReference = this;
        dieInstance.Roll();
    }

    public void SetAbility(Ability ability) 
    {
        characterPanel.resultImage.sprite = ability.UIImage;
    }
    private void Awake()
    {
        HP = maxHP;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    AbilityDie die;
    public AbilityDie dieReference;
    [SerializeField]
    public Vector2 spriteOffset;
    [SerializeField]
    public Color mainColor;
    [SerializeField]
    public int maxHP;
    public int HP;
    public int power;
    public Texture portrait;
    public Ability[] baseAbilities;
    public Ability[] abilities;
    public CharacterPanel characterPanel;
    public Ability currentAbility;
    public bool targetable = true;
    public Character instance;
    public GameObject indicator;
    public GameManager gameManager;
    
    public virtual void Roll()
    {
        CleanUp();
        AbilityDie dieInstance = Instantiate(die);
        dieReference = dieInstance;
        dieInstance.Initialise(this);
        dieInstance.Roll();
    }
    public void SetAbility(int value) 
    {
        if (value < 0)
        {
            currentAbility = null;
            characterPanel.resultImage.sprite = null;
        }
        else
        {
            currentAbility = abilities[value];
            abilities[value].characterReference = instance;
            characterPanel.resultImage.sprite = currentAbility.UIImage;
        }
    }

    public void PrimeAbility()
    {
        currentAbility.Activate();
    }

    public void ChangeHP(int value)
    {
        HP += value;
        Mathf.Clamp(HP, 0, maxHP);
        characterPanel.SetHP(HP);
        if(HP<=0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void OnAbilityUsed()
    {

    }

    public virtual void OnTurnStart()
    {

    }
    public virtual void CleanUp()
    {
        SetAbility(-1);
        if (dieReference != null) { Destroy(dieReference.gameObject); }
    }
    private void Awake()
    {
        for (int i = 0; i < baseAbilities.Length; i++)
        {
            abilities[i] = Instantiate(baseAbilities[i], this.transform);
        }
    }
    public void Initialise(Character temp)
    {
        instance = temp;
        HP = maxHP;
        characterPanel.gameObject.SetActive(true);
        characterPanel.Initialise(this);
    }
}

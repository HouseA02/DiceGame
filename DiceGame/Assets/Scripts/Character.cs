using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    public Target targetSprite;
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Transform damageNumber;
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
    public int block;
    public int power;
    public List<Character> allies;
    public List<Character> enemies;
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
    public virtual void SetAbility(int value) 
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
        var damagePopup = Instantiate(damageNumber, characterPanel.HPText.transform);
        damagePopup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f,1f),2) * 10000);
        if (value > 0) { damagePopup.GetComponent<TMP_Text>().color = Color.green; }
        damagePopup.GetComponent<TMP_Text>().text = Mathf.Abs(value).ToString();
        if(HP<=0)
        {
            Die();
        }
    }

    public void ChangeBlock(int value, bool isOverflowing)
    {
        block += value;
        if(block <= 0)
        {
            characterPanel.blockContainer.SetActive(false);
            if(isOverflowing) { ChangeHP(block); }
            block = 0;
        }
        else
        {
            characterPanel.blockContainer.SetActive(true);
            characterPanel.blockText.text = block.ToString();
        }
    }

    public void TakeDamage(int value)
    {
        ChangeBlock(-value, true);
    }
    public virtual void Die()
    {
        CleanUp();
        gameManager.OnDeath(this);
        gameObject.SetActive(false);
    }

    public virtual void OnAbilityUsed()
    {

    }

    public virtual void OnTurnStart()
    {
        ChangeBlock(-block, false);
    }

    public virtual void OnTurnEnd()
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
    public virtual void Initialise(Character temp)
    {
        instance = temp;
        HP = maxHP;
        characterPanel.gameObject.SetActive(true);
        characterPanel.Initialise(this);
    }
}

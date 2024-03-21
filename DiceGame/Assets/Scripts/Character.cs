using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Serializable]

    public class ResultEvent : UnityEvent<Ability> { }
    public class IntEvent : UnityEvent<int> { }

    [Serializable]
    
    public class StartStatus
    {
        public StatusEffect effect;
        public int value;
    }
    [SerializeField]
    private Tutorial tutorial;
    [SerializeField]
    private GameObject model;
    private AudioSource audioSource;
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
    public float spriteSize;
    [SerializeField]
    public Color mainColor;
    [SerializeField]
    public int id;
    public int maxHP;
    public int HP;
    public int block;
    public int power = 0;
    public int damageBlock = 0;
    public int thisTurnRolls = 0;
    public float damageMultiplier = 1;
    public List<Character> allies;
    public List<Character> enemies;
    public Sprite portrait;
    public Ability[] baseAbilities;
    public Ability[] abilities = new Ability[6];
    public List<Ability> resetAbilities = new List<Ability>();
    public CharacterPanel characterPanel;
    public Ability currentAbility;
    public bool targetable = true;
    public bool isFirstRoll = true;
    public bool canRoll = true;
    public bool isDead = false;
    public bool isBlind = false;
    public Character instance;
    public GameObject indicator;
    public GameManager gameManager;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    [SerializeField]
    protected List<StartStatus> startingStatuses = new List<StartStatus>();
    public UnityEvent m_OnTurnStart = new UnityEvent();
    public UnityEvent m_OnTurnEnd = new UnityEvent();
    public UnityEvent m_OnAttacked = new UnityEvent();
    public UnityEvent m_OnRoll = new UnityEvent();
    public UnityEvent m_OnReroll = new UnityEvent();
    public ResultEvent m_OnResult = new ResultEvent();
    public ResultEvent m_OnAbilityUsed = new ResultEvent();
    public IntEvent m_OnHeal = new IntEvent();
    public IntEvent m_OnLoseHP = new IntEvent();

    void Start()
    {
        tutorial = FindObjectOfType<Tutorial>();
    }

    public virtual void Roll()
    {
        thisTurnRolls++;
        if (thisTurnRolls == 1|| canRoll)
        {
            CleanUp();
            AbilityDie dieInstance = Instantiate(die);
            dieReference = dieInstance;
            dieInstance.transform.position = gameManager.dieOrigin.position;
            dieInstance.Initialise(this);
            dieInstance.Roll();
            isFirstRoll = false;
        }
        if (thisTurnRolls>1)
        {
            m_OnReroll.Invoke();
        }
        m_OnRoll.Invoke();
    }
    public virtual void SetAbility(int value) 
    {
        if (value < 0)
        {
            currentAbility = null;
            characterPanel.resultImage.sprite = null;
            characterPanel.resultImage.gameObject.SetActive(false);
            if(characterPanel.descContainer != null) { characterPanel.descContainer.SetActive(false); }
        }
        else
        {
            currentAbility = abilities[value];
            abilities[value].characterReference = instance;
            characterPanel.resultImage.gameObject.SetActive(true);
            characterPanel.resultImage.sprite = currentAbility.UIImage;
            characterPanel.resultImage.GetComponent<DieSpreadImage>().Initialise(currentAbility);
            /*if (characterPanel.descContainer != null) 
            { 
                characterPanel.descContainer.SetActive(true);
                characterPanel.descriptionText.text = currentAbility.description;
                characterPanel.abilityNameText.text = currentAbility.abilityName;
            }*/
            m_OnResult.Invoke(currentAbility);
            currentAbility.OnSet();
        }
    }



    public void PrimeAbility()
    {
        currentAbility.Activate();
    }

    public void ChangeHP(int value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, maxHP);
        characterPanel.SetHP(HP);
        var damagePopup = Instantiate(damageNumber, characterPanel.HPText.transform);
        damagePopup.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-1f,1f),2) * 10000);
        if (value > 0) 
        {
            damagePopup.GetComponent<TMP_Text>().color = Color.green;
            m_OnHeal.Invoke(value);
        }
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
        //damagetotake
        float damageToTake = value;
        damageToTake -= damageBlock;
        damageToTake *= damageMultiplier;
        m_OnAttacked.Invoke();
        ChangeBlock(-(int)damageToTake, true);
    }
    public virtual void Die()
    {
        isDead = true;
        targetable = false;
        CleanUp();
        Cleanse();
        statusEffects.Clear();
        characterPanel.deathOverlay.SetActive(true);
        gameManager.OnDeath(this);
        model.SetActive(false);
        //gameObject.SetActive(false);
    }

    public virtual void OnAbilityUsed(Ability ability)
    {
        m_OnAbilityUsed.Invoke(ability);
    }

    public virtual void OnTurnStart()
    {
        ChangeBlock(-block, false);
        m_OnTurnStart.Invoke();
    }

    public virtual void OnTurnEnd()
    {
        m_OnTurnEnd.Invoke();
        thisTurnRolls = 0;
    }

    public virtual void ApplyStatus(StatusEffect status, int value)
    {
        if (statusEffects.Find(s => s.statusName == status.statusName) != null)
        {
            foreach(StatusEffect statusEffect in statusEffects.Where(s => s.statusName == status.statusName))
            {
                statusEffect.AddValue(value);
            }
        }
        else
        {
            StatusEffect newStatus = Instantiate(status);
            newStatus.Initialise(this, value);
            statusEffects.Add(newStatus);
            foreach (StatusSlot slot in characterPanel.statusSlots)
            {
                if (!slot.isTaken)
                {
                    slot.Initialise(newStatus, value);
                    break;
                }
            }
        }
    }

    public void ResetAbilities()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            AddAbility(resetAbilities[i], i);
        }
    }

    public void SetResetAbilites()
    {
        resetAbilities = new List<Ability>();
        resetAbilities.AddRange(abilities);

    }
    public void Cleanse()
    {
        while(statusEffects.Count > 0)
        {
            statusEffects[0].Expire();
        }
    }
    public virtual void CleanUp()
    {
        SetAbility(-1);
        if (dieReference != null) { Destroy(dieReference.gameObject); }
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < baseAbilities.Length; i++)
        {
            abilities[i] = Instantiate(baseAbilities[i], this.transform);
            abilities[i].id = i;
        }
        power = 0;
    }

    public void AddAbility(Ability ability, int faceTarget)
    {
        abilities[faceTarget] = Instantiate(ability, this.transform);
        abilities[faceTarget].id = faceTarget;
        characterPanel.dieSpreadImages[faceTarget].Initialise(ability);
    }
    public virtual void Initialise(Character temp)
    {
        instance = temp;
        HP = maxHP;
        damageMultiplier = 1;
        characterPanel.gameObject.SetActive(true);
        characterPanel.Initialise(this);
        gameManager.gm_OnTurnStart.AddListener(OnTurnStart);
        gameManager.gm_OnTurnEnd.AddListener(OnTurnEnd);
        m_OnResult.AddListener(OnResult);
        startingStatuses.ForEach(s => ApplyStatus(s.effect, s.value));
    }

    void OnResult(Ability ability)
    {
        if (ability != null)
        {
            Debug.Log(ability.abilityName);
        }
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        audioSource.PlayOneShot(sound, volume);
    }

    public void PlaySound(AudioClip sound, float volume, float pitch)
    {
        audioSource.PlayOneShot(sound, volume, pitch);
    }
}

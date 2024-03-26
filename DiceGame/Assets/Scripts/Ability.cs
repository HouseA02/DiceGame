using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Ability : MonoBehaviour
{
    public int id;
    public string abilityName;
    [SerializeField]
    public bool targetsEnemy;
    [SerializeField]
    public bool targetsAlly;
    [SerializeField]
    public bool targetsSelf;
    public enum TargetingType
    {
        none,
        single,
        team,
        all,
        self
    }
    [SerializeField]
    public List<AbilityEffect> effects = new List<AbilityEffect>();
    public List<AbilityEffect> cantripEffects = new List<AbilityEffect>();
    public TargetingType targetingType;
    [SerializeField]
    public Material image;
    [SerializeField]
    public Sprite UIImage;
    [SerializeField]
    public DecalProjector decal;
    [SerializeField]
    public string description;
    public GameManager gameManager;
    public Character characterReference;
    public float delay = 0.2f;
    [HideInInspector]
    public int lastHitDamage = 0;
    [HideInInspector]
    public int totalDamage = 0;
    public virtual void Activate()
    {
        lastHitDamage = 0;
        totalDamage = 0;
    }

    public virtual IEnumerator UseAbility()
    {
        characterReference.CleanUp();
        //effects.ForEach(e => { e.Activate(characterReference, target); });
        foreach (AbilityEffect effect in effects)
        {
            effect.Activate(characterReference, characterReference);
            yield return new WaitForSeconds(delay);
        }
        characterReference.OnAbilityUsed(this);
    }
    public virtual IEnumerator UseAbility(Character target)
    {
        characterReference.CleanUp();
        //effects.ForEach(e => { e.Activate(characterReference, target); });
        foreach (AbilityEffect effect in effects)
        {
            effect.Activate(characterReference, target);
            characterReference.transform.LookAt(target.transform.position);
            yield return new WaitForSeconds(0.2f);
        }
        characterReference.OnAbilityUsed(this);
    }
    public virtual IEnumerator UseAbility(List<Character> targets)
    {
        characterReference.CleanUp();
        //effects.ForEach(e => { e.Activate(characterReference, target); });
        foreach (AbilityEffect effect in effects)
        {
            effect.Activate(characterReference, targets);

            yield return new WaitForSeconds(0.2f);
        }
        characterReference.OnAbilityUsed(this);
    }
    public void OnSet()
    {
        StartCoroutine(OnSetEffect());
    }

    protected virtual IEnumerator OnSetEffect()
    {
        Debug.Log("active");
        foreach (AbilityEffect effect in cantripEffects)
        {
            effect.Activate(characterReference, characterReference);
            yield return new WaitForSeconds(0.2f);
        }
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

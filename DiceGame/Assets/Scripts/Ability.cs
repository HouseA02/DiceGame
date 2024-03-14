using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class Ability : MonoBehaviour
{
    [SerializeField]
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
    public virtual void Activate()
    {

    }

    public virtual IEnumerator UseAbility()
    {
        characterReference.CleanUp();
        //effects.ForEach(e => { e.Activate(characterReference, target); });
        foreach (AbilityEffect effect in effects)
        {
            effect.Activate(characterReference, characterReference);
            yield return new WaitForSeconds(0.2f);
        }
        characterReference.OnAbilityUsed();
    }
    public virtual IEnumerator UseAbility(Character target)
    {
        characterReference.CleanUp();
        //effects.ForEach(e => { e.Activate(characterReference, target); });
        foreach (AbilityEffect effect in effects)
        {
            effect.Activate(characterReference, target);
            yield return new WaitForSeconds(0.2f);
        }
        characterReference.OnAbilityUsed();
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
        characterReference.OnAbilityUsed();
    }



    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

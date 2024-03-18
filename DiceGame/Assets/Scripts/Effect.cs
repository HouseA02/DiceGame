using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class Effect : MonoBehaviour
{
    protected GameManager gameManager;
    public GameObject VFX;
    public AudioClip sound;
    public float soundVolumeScale = 1.0f;
    public Vector2 pitchRange = new (0.7f,1.3f);
    public Ability abilityReference;

    [SerializeField]
    protected Vector3 offset = new Vector3(0, 1, 0);
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (GetComponent<Ability>() != null )
        {
            abilityReference = GetComponent<Ability>();
        }
    }
    public virtual void Activate(Character source, Character target, float value)
    {
        if (VFX != null && target.isActiveAndEnabled == true)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
        if (sound != null)
        {
            target.PlaySound(sound, soundVolumeScale, Random.Range(pitchRange.x, pitchRange.y));
        }
    }

    public virtual void Activate(Character source, List<Character> targets, float value)
    {

    }

    public virtual void Activate(float value)
    {

    }

    public virtual void Activate(Character target, float value)
    {
        if (VFX != null && target.isActiveAndEnabled == true)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
        if (sound != null)
        {
            target.PlaySound(sound, soundVolumeScale, Random.Range(pitchRange.x, pitchRange.y));
        }
    }

    public virtual void Activate(Character target)
    {
        if (VFX != null && target.isActiveAndEnabled == true)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
        if (sound != null)
        {
            target.PlaySound(sound, soundVolumeScale, Random.Range(pitchRange.x, pitchRange.y));
        }
    }

    public virtual void Activate(Character source, Character target)
    {
        if (VFX != null && target.isActiveAndEnabled == true)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
        if (sound != null)
        {
            target.PlaySound(sound, soundVolumeScale, Random.Range(pitchRange.x, pitchRange.y));
        }
    }
    public virtual void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
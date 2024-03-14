using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.VFX;

public class Effect : MonoBehaviour
{
    protected GameManager gameManager;
    public VisualEffect VFX;
    [SerializeField]
    protected Vector3 offset = new Vector3(0,1,0);
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public virtual void Activate(Character source, Character target, float value)
    {
        if (VFX != null)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
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
        if (VFX != null)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
    }

    public virtual void Activate(Character target)
    {
        if (VFX != null)
        {
            Instantiate(VFX, target.transform.position + offset, Quaternion.identity);
        }
    }
    public virtual void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

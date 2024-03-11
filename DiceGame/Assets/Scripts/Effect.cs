using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Effect : MonoBehaviour
{
    protected GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public virtual void Activate(Character source, Character target, float value)
    {

    }

    public virtual void Activate(Character source, List<Character> targets, float value)
    {

    }

    public virtual void Activate(float value)
    {

    }

    public virtual void Activate(Character target, float value) 
    {

    }

    public virtual void Activate(Character target)
    {

    }
    public virtual void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

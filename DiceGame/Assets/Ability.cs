using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField]
    public Material image;
    [SerializeField]
    public string debugText;
    public virtual void Activate()
    {
        Debug.Log(debugText);
    }
}

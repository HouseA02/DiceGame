using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFace : MonoBehaviour
{
    [SerializeField]
    public Ability ability;
    public Material material;
    private void Awake()
    {
        material = GetComponent<Material>();
        //if (ability != null) { material = ability.image; }
    }

    public void Activate()
    {
        ability.Activate();
    }
}


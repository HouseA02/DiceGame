using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField]
    float lifeSpan;
    private void FixedUpdate()
    {
        if (lifeSpan < 0) 
        {
            Destroy(gameObject);
        }
        lifeSpan--;
    }
}

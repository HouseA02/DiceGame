using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceCheck : MonoBehaviour
{
    Die die;
    public int faceValue;

    private void Awake()
    {
        die = GetComponentInParent<Die>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (die != null) 
        {
            die.value = faceValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (die != null)
        {
            die.value = -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{   
    void Update()
    {
        // Rotate the object around its local y axis at 1 degree per second
        transform.Rotate(Vector3.forward * Time.deltaTime*5);
    }
}




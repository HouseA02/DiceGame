using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField]
    float force;
    [SerializeField]
    float torque;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Roll();
        }
    }

    private void Roll()
    {
        Vector3 target = new Vector3(Random.Range(-2f, 2f), 5f, Random.Range(-2f, 2f));
        Vector3 dir = target - transform.position;
        rb.AddForce(dir * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
    }
}

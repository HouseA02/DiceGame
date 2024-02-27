using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Die : MonoBehaviour
{
    [SerializeField]
    float force;
    [SerializeField]
    float torque;
    [SerializeField]
    public BoxCollider[] faceChecks;
    public int value;
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
        StartCoroutine("WaitForResult");
    }
    private IEnumerator WaitForResult()
    {
        yield return new WaitUntil(rb.IsSleeping);
        Debug.Log($"You rolled {value}!");
        if(value == -1)
        {
            value= 0;
            Roll();
        }
    }

}

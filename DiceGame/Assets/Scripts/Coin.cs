using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Die
{
    public override void Roll()
    {
        float variance;
        variance = Random.Range(-3f, 3f);
        Vector3 target = Vector3.up;
        Vector3 dir = target - transform.position;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * (torque + variance), ForceMode.Impulse);
        StartCoroutine("WaitForResult");
    }
}

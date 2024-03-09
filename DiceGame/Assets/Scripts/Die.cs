using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class Die : MonoBehaviour
{
    [SerializeField]
    public float force;
    [SerializeField]
    public float torque;
    [SerializeField]
    public BoxCollider[] faceChecks;
    public int value;
    public Rigidbody rb;
    AudioSource audioSource;
    public AudioClip[] sounds;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Roll()
    {
        Vector3 target = transform.position + new Vector3(Random.Range(-2f, 2f), 5f, Random.Range(-2f, 2f));
        Vector3 dir = target - transform.position;
        rb.AddForce(dir * force, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
        StartCoroutine("WaitForResult");
    }
    public virtual IEnumerator WaitForResult()
    {
        yield return new WaitUntil(rb.IsSleeping);
        if(value < 1)
        {
            value= 0;
            Roll();
        }
        else
        {
            Activate(value);
            rb.isKinematic = true;
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sounds != null && audioSource != null)
        {
            if(rb.velocity.magnitude > 10) 
            { 
                audioSource.clip = sounds[2];
            }
            else if(rb.velocity.magnitude > 3)
            {
                audioSource.clip = sounds[1];
            }
            else
            {
                audioSource.clip = sounds[0];
            }
            audioSource.Play();
        }   
    }
    public virtual void Activate(int value)
    {
        Debug.Log($"You rolled {value}!");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField]
    private float flySpeed;
    [SerializeField]
    private float flyRange;
    private float maxHeight;
    private float minHeight;
    private float yPos;
    private float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
        maxHeight = transform.position.y + flyRange;
        minHeight = transform.position.y - flyRange;
    }

    // Update is called once per frame
    void Update()
    {
        yPos = Mathf.Lerp(minHeight, maxHeight, t);
        t = Mathf.PingPong(Time.time, 1);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}

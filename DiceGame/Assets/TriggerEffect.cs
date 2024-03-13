using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerEffect : MonoBehaviour
{
    [SerializeField]
    public Image image;
    private void FixedUpdate()
    {
        transform.localScale *= 1.1f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.05f);
        if(image.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}

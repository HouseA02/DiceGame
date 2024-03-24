using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AbilityVFX : MonoBehaviour
{
    private VisualEffect effect;
    private int timer;
    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
        timer = 60;
    }

    private void Update()
    {
        if (effect.aliveParticleCount == 0 && timer <= 0)
        {
            Destroy(gameObject);
        }
        timer--;
    }
}

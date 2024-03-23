using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class EffectTest : MonoBehaviour
{
    [SerializeField]
    public VisualEffect effect;

    private void Awake()
    {
        effect = GetComponent<VisualEffect>();
        effect.SetSkinnedMeshRenderer("TargetMesh_", GetComponentInParent<SkinnedMeshRenderer>());
    }
}
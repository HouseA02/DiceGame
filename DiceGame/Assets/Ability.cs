using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ability : MonoBehaviour
{
    [SerializeField]
    public Material image;
    [SerializeField]
    public Sprite UIImage;
    [SerializeField]
    public DecalProjector decal;
    [SerializeField]
    public string debugText;
    public virtual void Activate()
    {
        Debug.Log(debugText);
    }

    private void Awake()
    {
        decal = GetComponent<DecalProjector>();
        decal.material = image;
    }
}

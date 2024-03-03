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
    public GameManager gameManager;
    public Character characterReference;
    public virtual void Activate()
    {
        
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}

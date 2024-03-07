using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float Strength;
    public float Speed;
    public float Attack;

    public Text strengthText;
    public Text speedText;
    public Text attackText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        strengthText.text = Strength.ToString();
        speedText.text = Speed.ToString();
        attackText.text = Attack.ToString();
    }
}

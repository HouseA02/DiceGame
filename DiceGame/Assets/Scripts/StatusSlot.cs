using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class StatusSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text valueText;
    public bool isTaken = false;

    public void Initialise(StatusEffect statusEffect, int value)
    {
        statusEffect.slot = this;
        this.gameObject.SetActive(true);
        image.sprite = statusEffect.sprite;
        valueText.text = value.ToString();
        isTaken = true;
    }

    public void UpdateValue(int value)
    {
        if (value > 0)
        {
            valueText.text = value.ToString();
        }
        else
        {
            isTaken = false;
            image.sprite = null;
            gameObject.SetActive(false);
        }

    }
}

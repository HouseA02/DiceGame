using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusEntry : CompendiumEntry
{
    public Image imageSlot;
    public Image imageSlot2;
    public TMP_Text descText;
    public void Initialise(StatusEffect status)
    {
        imageSlot.sprite = status.sprite;
        imageSlot2.sprite = status.sprite;
        descText.text = status.statusName.ToString() + ": " + status.description;
    }
}

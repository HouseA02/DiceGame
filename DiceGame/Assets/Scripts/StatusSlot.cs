using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class StatusSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StatusEffect statusReference;
    public Image image;
    public TMP_Text valueText;
    public bool isTaken = false;
    public GameObject infoPrefab;
    private GameObject infoInstance;
    public string statusDesc;
    public string baseDesc;
    [SerializeField]
    private Vector3 infoOffset;
    [SerializeField]
    private Color valueColor;
    [SerializeField]
    private GameObject[] parts;
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoInstance = Instantiate(infoPrefab, this.transform);
        infoInstance.transform.position = this.transform.position + infoOffset;
        infoInstance.GetComponentInChildren<TMP_Text>().text = statusDesc;
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(infoInstance != null) { Destroy(infoInstance); }
    }
    public void Initialise(StatusEffect statusEffect, int value)
    {
        statusEffect.slot = this;
        statusReference = statusEffect;
        baseDesc = statusEffect.description;
        valueColor = statusEffect.valueColor;
        valueText.text = value.ToString();
        string coloredValue = $"<b><color=#" + valueColor.ToHexString() + ">" + value + "</color></b>";
        statusDesc = string.Format(statusEffect.description, coloredValue);
        foreach(var part in parts)
        {
            part.SetActive(true);
        }
        image.sprite = statusEffect.sprite;
        valueText.text = value.ToString();
        statusEffect.OnApplied();
        isTaken = true;
    }

    public void UpdateValue(int value)
    {
        if (value > 0)
        {
            valueText.text = value.ToString();
            string coloredValue = $"<b><color=#" + valueColor.ToHexString() + ">" + value + "</color></b>";
            statusDesc = string.Format(baseDesc, coloredValue);
        }
        else
        {
            Expire();
        }

    }

    public void Expire()
    {
        isTaken = false;
        image.sprite = null;
        foreach(var part in parts)
        {
            part.SetActive(false);
        }
    }
}

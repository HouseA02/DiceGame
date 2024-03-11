using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class RelicSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image imageSlot;
    public TMP_Text valueSlot;
    public GameObject description;
    public TMP_Text nameText;
    public TMP_Text descText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }
}

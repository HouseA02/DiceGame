using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DieSpreadImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image image;
    [SerializeField]
    GameObject description;
    [SerializeField]
    TMP_Text descText;
    [SerializeField]
    TMP_Text nameText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }

    public void Hide()
    {
        description.SetActive(false);
    }
    public void Initialise(Ability ability)
    {
        image.sprite = ability.UIImage;
        descText.text = ability.description;
        nameText.text = ability.abilityName;
    }
}

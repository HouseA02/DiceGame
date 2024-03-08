using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Event : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public EventArenaController eventArenaController;
    [SerializeField]
    public EventType eventType;
    [SerializeField]
    private SpriteRenderer[] sprites;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    public int minimumSteps;

    public int id;
    bool inFocus = false;

    private void Awake()
    {
        sprites[1].color = defaultColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inFocus)
        {
            sprites[1].color = Color.green;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprites[1].color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inFocus)
        {
            eventArenaController.StartEvent(this);
        }
    }
    public void Focus(bool isActive)
    {
        switch(isActive)
        {
            case true:
                inFocus = true;
                for (int i = 0; i < sprites.Length; i++)
                {
                    sprites[i].sortingOrder = i+10;
                }
                break;
            case false:
                inFocus = false;
                foreach (SpriteRenderer sprite in sprites)
                {
                    sprite.sortingOrder = 0;
                }
                break;
        }
    }
    public enum EventType
    {
        None,
        Combat,
        Elite,
        Rest,
        Story,
        Boss
    }

}

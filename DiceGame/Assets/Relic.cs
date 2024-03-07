using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.TextCore.Text;

public class Relic : MonoBehaviour
{
    public string relicName;
    public GameManager gameManager;
    bool usesValue;
    [SerializeField]
    int initialValue;
    [SerializeField]
    public AbilityEffect[] effects;
    public RelicSlot slot;
    public Sprite image;
    public virtual void Initialise(GameManager gm)
    {
        gameManager = gm;
        if (image != null) { slot.imageSlot.sprite = image; }
        if (usesValue) { slot.valueSlot.gameObject.SetActive(true); slot.valueSlot.text = initialValue.ToString(); }
        gm.gm_OnTurnStart.AddListener(OnTurnStart);
        gm.gm_OnTurnEnd.AddListener(OnTurnEnd);
        OnPickUp();
    }

    protected virtual void OnTurnStart()
    {

    }

    protected virtual void OnTurnEnd()
    {

    }

    protected virtual void OnPickUp()
    {

    }

    protected virtual void UpdateValue()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.TextCore.Text;

public class Relic : Reward
{
    public string relicName;
    public string relicDescription;
    public GameManager gameManager;
    bool usesValue;
    [SerializeField]
    int initialValue;
    [SerializeField]
    public AbilityEffect[] effects;
    public RelicSlot slot;
    public Sprite image;
    public UnityEvent r_OnTurnStart = new UnityEvent();
    public UnityEvent r_OnBattleStart = new UnityEvent();
    public UnityEvent r_OnTurnEnd = new UnityEvent();
    public virtual void Initialise(GameManager gm)
    {
        gameManager = gm;
        if (image != null) { slot.imageSlot.sprite = image; }
        if (usesValue) { slot.valueSlot.gameObject.SetActive(true); slot.valueSlot.text = initialValue.ToString(); }
        slot.descText.text = relicDescription;
        slot.nameText.text = relicName;
        gm.gm_OnTurnStart.AddListener(OnTurnStart);
        gm.gm_OnBattleStart.AddListener(OnBattleStart);
        gm.gm_OnTurnEnd.AddListener(OnTurnEnd);
        OnPickUp();
    }
    protected virtual void OnBattleStart()
    {
        r_OnBattleStart.Invoke();
    }
    protected virtual void OnTurnStart()
    {
        r_OnTurnStart.Invoke();
    }

    protected virtual void OnTurnEnd()
    {
        r_OnTurnStart.Invoke();
    }

    protected virtual void OnPickUp()
    {

    }

    protected virtual void UpdateValue()
    {

    }
}

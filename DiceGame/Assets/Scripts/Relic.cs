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
    private Player player;
    bool usesValue;
    [SerializeField]
    int initialValue;
    public int currentValue =0;
    bool onBattleStart = true;
    public bool stackable;
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
        player = gameManager.player;
        currentValue = initialValue;
        foreach(Relic relic in player.relics)
        {
            if (relic != null && relic.relicName == this.relicName && stackable)
            {
                relic.currentValue += currentValue;
                Remove();
            }
        }
        if (image != null) { slot.imageSlot.sprite = image; }
        if (usesValue) { slot.valueSlot.gameObject.SetActive(true); slot.valueSlot.text = currentValue.ToString(); }
        slot.descText.text = relicDescription;
        slot.nameText.text = relicName;
        gm.gm_OnBattleStart.AddListener(OnTrueBattleStart);
        gm.gm_OnTurnStart.AddListener(OnTurnStart);
        gm.gm_OnTurnStart.AddListener(OnBattleStart);
        gm.gm_OnTurnEnd.AddListener(OnTurnEnd);
        OnPickUp();
    }

    protected virtual void OnTrueBattleStart()
    {
        onBattleStart = true;
    }
    protected virtual void OnBattleStart()
    {
        if (onBattleStart)
        {
            r_OnBattleStart.Invoke();
        }
        onBattleStart = false;
    }
    protected virtual void OnTurnStart()
    {
        r_OnTurnStart.Invoke();
    }

    protected virtual void OnTurnEnd()
    {
        r_OnTurnEnd.Invoke();
    }

    protected virtual void OnPickUp()
    {

    }

    public virtual void Remove()
    {
        player.relics.Remove(this);
        Destroy(slot.gameObject);
        Destroy(gameObject);
    }
    protected virtual void UpdateValue()
    {

    }
}

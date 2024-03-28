using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrantReward : Effect
{
    [SerializeField]
    public Reward reward;
    public override void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("Activated");
        switch (reward.rewardType)
        {
            case Reward.RewardType.Relic:
                gameManager.player.AddRelic((Relic)reward);
                break;
            case Reward.RewardType.Face:
                LootScreen lootScreen = gameManager.lootScreen;
                lootScreen.enabled = true;
                lootScreen.panel.SetActive(false);
                lootScreen.ClaimFace((Ability)reward);
                break;
        }
        base.Activate();
    }
}

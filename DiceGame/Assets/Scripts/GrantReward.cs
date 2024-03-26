using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrantReward : Effect
{
    [SerializeField]
    public Reward[] rewards;
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
                LootScreen lootScreen = FindFirstObjectByType<LootScreen>();
                lootScreen.ClaimFace((FaceReward)reward);
                break;
        }
        base.Activate();
    }

    public override void Activate(float value)
    {
        reward = rewards[(int)value];
        base.Activate(value);
    }
}

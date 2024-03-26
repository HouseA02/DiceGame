using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiReward : GrantReward
{
    public List<Reward> Rewards;
    public override void Activate()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Debug.Log("Activated");
        foreach (var reward in Rewards)
        {
            switch (reward.rewardType)
            {
                case Reward.RewardType.Relic:
                    gameManager.player.AddRelic((Relic)reward);
                    break;
            }
        }
        base.Activate();
    }
}

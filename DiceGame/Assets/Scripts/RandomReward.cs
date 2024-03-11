using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomReward : GrantReward
{
    [SerializeField]
    private List<Reward> potentialRewards;
    public override void Activate()
    {
        reward = potentialRewards[Random.Range(0, potentialRewards.Count)];
        base.Activate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public enum RewardType
    {
        None,
        Relic,
        Face
    }

    public RewardType rewardType;
}

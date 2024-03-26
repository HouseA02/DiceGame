using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FaceReward : Reward
{
    [SerializeField]
    public string name;
    public Hero.Class pool;
    public LootManager.Rarity rarity;
    public Ability face;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour 
{
    [System.Serializable]
    public class LootList
    {
        [SerializeField]
        public CombatData.CombatType type;
        [SerializeField]
        public int soulsMin;
        [SerializeField]
        public int soulsMax;
    }
    public List<LootList> loot = new List<LootList>();
}

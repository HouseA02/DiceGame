using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootManager : MonoBehaviour 
{
    public AudioSource audioSource;
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        epic
    }
    [System.Serializable]
    public class SoulBounty
    {
        [SerializeField]
        public CombatData.CombatType type;
        [SerializeField]
        public int soulsMin;
        [SerializeField]
        public int soulsMax;
    }
    [System.Serializable]
    public class LootFace
    {
        [SerializeField]
        public string name;
        public Hero.Class pool;
        public Rarity rarity;
        public Ability face;
    }
    public List<SoulBounty> soulBounty = new List<SoulBounty>();
    public List<LootFace> fullFacePool = new List<LootFace>();
    public List<LootFace> facePool = new List<LootFace>();

    public void AddFacePool(Hero.Class heroClass)
    {
        foreach (var face in fullFacePool.Where(face => face.pool == heroClass))
        {
            facePool.Add(face);
        }
    }
}

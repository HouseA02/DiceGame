using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootManager : MonoBehaviour 
{
    public AudioSource audioSource;

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

    public List<SoulBounty> soulBounty = new List<SoulBounty>();
    public List<Ability> fullFacePool = new List<Ability>();
    public List<Ability> facePool = new List<Ability>();

    public void AddFacePool(Hero.Class heroClass)
    {
        foreach (var face in fullFacePool.Where(face => face.pool == heroClass))
        {
            facePool.Add(face);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public RelicPlacer relicPlacer;
    public GameManager gameManager;
    public List<Relic> relics = new List<Relic>();
    [SerializeField]
    public List<Relic> tempRelics;
    public void AddRelic(Relic relic)
    {
        Relic relicInstance = Instantiate(relic, this.transform);
        relicPlacer.PlaceRelic(relicInstance);
        relicInstance.Initialise(gameManager);
        relics.Add(relicInstance);
    }
}

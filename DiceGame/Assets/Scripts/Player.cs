using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public RelicPlacer relicPlacer;
    public GameManager gameManager;
    public List<Relic> relics = new List<Relic>();
    [SerializeField]
    public Relic starterRelic;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        gameManager.gm_OnBattleStart.AddListener(StartRelic);
    }
    public void AddRelic(Relic relic)
    {
        Relic relicInstance = Instantiate(relic, this.transform);
        relicPlacer.PlaceRelic(relicInstance);
        relicInstance.Initialise(gameManager);
        relics.Add(relicInstance);
    }

    void StartRelic()
    {
        if (starterRelic != null)
        {
            AddRelic(starterRelic);
            starterRelic = null;
        }
    }
}

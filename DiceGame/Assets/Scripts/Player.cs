using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int souls = 0;
    public TMP_Text soulsText;
    public RelicPlacer relicPlacer;
    public GameManager gameManager;
    public List<Relic> relics = new List<Relic>();
    [SerializeField]
    public Relic starterRelic;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
        gameManager.gm_OnBattleStart.AddListener(StartRelic);
        ChangeSouls(0);
        LoadData();
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            relics[0].Remove();
        }
    }
    public void ChangeSouls(int amount)
    {
        souls += amount;
        soulsText.text = souls.ToString();
    }

    public void LoadData()
    {
        SaveData data = SaveManager.LoadData();
        souls = data.souls;
        ChangeSouls(0);
    }
}

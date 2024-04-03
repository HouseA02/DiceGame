using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventArenaController : MonoBehaviour
{
    public class MapEvent : UnityEvent<Event> { }
    [SerializeField]
    private MapController mapController;
    [SerializeField]
    GameManager gameManager;
    public string eventName;
    public GameObject battleCanvas;

    public GameObject[] act1NormalEnemies;
    public GameObject[] act1EliteEnemies;
    public GameObject[] act2NormalEnemies;
    public GameObject[] act2EliteEnemies;
    public GameObject[] act3NormalEnemies;
    public GameObject[] act3EliteEnemies;

    public GameObject encounterParent;
    public Transform encounterCardPos;
    public GameObject[] Act1Encounters;
    public GameObject[] Act2Encounters;
    public GameObject[] Act3Encounters;
    private GameObject currentEncounter;

    public GameObject mainCam;
    public CanvasGroup cg;

    public MapEvent m_mapEvent = new MapEvent();
    public bool fadeIn;
    private Animator mapCamAnim;
    // Start is called before the first frame update
    void Start()
    {
        mapCamAnim = mainCam.GetComponent<Animator>();
    }

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (eventName == "NormalEnemyEvents")
        {
            NormalEnemyEvent();
        }
        else if (eventName == "EliteEvents")
        {
            EliteEvent();
        }
        else if (eventName == "RestEvents")
        {
            RestEvent();
        }
        else if (eventName == "EncounterEvents")
        {
            EncounterEvent();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            mapCamAnim.SetTrigger("Deactivate");
        }*/

        if (fadeIn)
        {
            cg.blocksRaycasts = true;
            cg.alpha += 1f * Time.deltaTime;
        }
        if (!fadeIn)
        {
            cg.alpha -= 1f * Time.deltaTime;
            cg.blocksRaycasts = false;
        }
    }

    public void StartEvent(Event e)
    {
        m_mapEvent.Invoke(e);
        switch(e.eventType)
        {
            case Event.EventType.Combat:
                NormalEnemyEvent((CombatEvent)e);
                break;
            case Event.EventType.Elite:
                EliteEvent((EliteEvent)e); 
                break;
            case Event.EventType.Boss:
                BossEvent((BossEvent)e);
                break;
            case Event.EventType.Rest:
                RestEvent((RestEvent)e);
                break;
            case Event.EventType.Story:
                EncounterEvent((StoryEvent)e);
                break;
        }
        mapController.Next(e);
    }
    void NormalEnemyEvent(CombatEvent e)
    {
        //mapCamAnim.SetTrigger("Activate");
        eventName = null;
        gameManager.StartCombat(e.combatData);
    }
    void EliteEvent(EliteEvent e)
    {
        //mapCamAnim.SetTrigger("Activate");
        eventName = null;
        gameManager.StartCombat(e.combatData);
    }
    void BossEvent(BossEvent e)
    {
        //mapCamAnim.SetTrigger("Activate");
        eventName = null;
        gameManager.StartCombat(e.combatData);
    }
    void RestEvent(RestEvent e)
    {
        //mapCamAnim.SetTrigger("Activate");
        fadeIn = true;
        eventName = null;
        gameManager.GetComponent<StoryController>().StartStory(e.storyData);
    }
    void EncounterEvent(StoryEvent e)
    {
        //mapCamAnim.SetTrigger("Activate");
        fadeIn = true;
        eventName = null;
        mainCam.gameObject.SetActive(false);
        gameManager.battleCamera.enabled = true;
        gameManager.mapKey.SetActive(false);
        battleCanvas.SetActive(false);
        gameManager.GetComponent<StoryController>().StartStory(e.storyData);
        //currentEncounter = Instantiate(Act1Encounters[Random.Range(0, Act1Encounters.Length)], encounterCardPos.position, Quaternion.identity, encounterParent.transform);
    }

    public void fadeOut()
    {
        if(!gameManager.inBattle)
        {
            mainCam.gameObject.SetActive(true);
            //gameManager.battleCamera.enabled = false;
            gameManager.mapKey.SetActive(true);
            battleCanvas.SetActive(true);
        }
        fadeIn = false;
        Destroy(currentEncounter);
    }
}

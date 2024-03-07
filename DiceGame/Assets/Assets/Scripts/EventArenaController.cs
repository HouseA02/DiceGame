using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventArenaController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    public string eventName;

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
        if (eventName == "NormalEnemyEvents")
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
        }

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

    void NormalEnemyEvent()
    {
        mapCamAnim.SetTrigger("Activate");
        eventName = null;
        gameManager.Initialise();
    }
    void EliteEvent()
    {
        mapCamAnim.SetTrigger("Activate");
        eventName = null;
    }
    void RestEvent()
    {
        mapCamAnim.SetTrigger("Activate");
        fadeIn = true;
        eventName = null;
    }
    void EncounterEvent()
    {
        mapCamAnim.SetTrigger("Activate");
        fadeIn = true;
        eventName = null;
        currentEncounter = Instantiate(Act1Encounters[Random.Range(0, Act1Encounters.Length)], encounterCardPos.position, Quaternion.identity, encounterParent.transform);
    }

    public void fadeOut()
    {
        fadeIn = false;
        Destroy(currentEncounter);
    }
}
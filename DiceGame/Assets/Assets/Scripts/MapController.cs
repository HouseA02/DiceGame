using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [System.Serializable]
    public class CombatList
    {
        public List<CombatData> combats = new List<CombatData>();
        public List<CombatData> elites = new List<CombatData>();
    }
    [SerializeField]
    public EventArenaController eventArenaController;
    public List<Event> Events = new List<Event>();
    public List<float> EventPercentages = new List<float>();

    [SerializeField]
    public Event newEvent;
    public float EventsNum;

    [SerializeField]
    public CombatList[] acts;
    private int act = 0;

    public float mapTime;
    public int lineNum;

    private float rotateAmount;
    private float handReset;
    private GameObject clockHand;

    [SerializeField]
    private CombatEvent combatEvent;
    [SerializeField]
    private EliteEvent eliteEvent;
    [SerializeField]
    private RestEvent restEvent;
    [SerializeField]
    private StoryEvent storyEvent;


    private List<Event> SpawnedEvents = new List<Event>();
    private List<Transform> Points = new List<Transform>();
    private List<Event> currentChoices = new List<Event>();
    private List<Event> possibleEvents = new List<Event>();

    public LineRenderer line;

    public bool reset;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            Points.Add(transform.GetChild(i));
        }

        line = GetComponent<LineRenderer>();
        clockHand = GameObject.Find("ClockHand");

        reset = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            reset = false;
            mapTime = 1;
            possibleEvents.Clear();
            populateList();
            line.positionCount = 0;
            lineNum = 0;
            clockHand.transform.RotateAround(transform.position, Vector3.up, handReset);
            handReset = 0;
            SpawnedEvents.ForEach(e => { Destroy(e.gameObject); });

            float handNum = 1;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    Event rand = possibleEvents[Random.Range(0, possibleEvents.Count)];
                    //GameObject instance = Instantiate(rand, Points[j].position, Quaternion.identity);
                    Event instance = Instantiate(rand, Points[j].position, Points[j].rotation);
                    instance.id = i+1;
                    possibleEvents.Remove(rand);
                    instance.eventArenaController = eventArenaController;
                    //instance.transform.SetParent(GameObject.Find(instance.id.ToString()).transform);
                    instance.tag = handNum.ToString();
                    SpawnedEvents.Add(instance);
                }
                rotateAmount += 30;
                transform.localRotation = Quaternion.Euler(0, rotateAmount, 0);
                handNum++;
            }
            foreach(Event e in SpawnedEvents.Where(e => e.eventType == Event.EventType.Combat))
            {
                e.GetComponent<CombatEvent>().combatData = acts[act].combats[Random.Range(0, acts[act].combats.Count)]; 
            }
            eventChoice();
        }
    }

    public void eventChoice()
    {
        foreach (Event e in currentChoices)
        {
            if(e != null)
            {
                e.transform.localScale /= 2;
                e.Focus(false);
                e.name = "DeadEvent";
            }
        }
        currentChoices.Clear();
        foreach (Event e in SpawnedEvents.Where(e => e.id == mapTime))
        {
            currentChoices.Add(e);
        }      
        foreach (Event e in currentChoices)
        {
            e.transform.localScale *= 2;
            e.Focus(true);
            e.name = "Choose";
        }
        clockHand.transform.RotateAround(transform.position, Vector3.up, 30);
        handReset -= 30;
    }

    public void Next(Event e)
    {
        line.positionCount++;
        mapTime++;
        eventChoice();
        line.SetPosition(lineNum, e.transform.position);
        lineNum++;
    }

    void populateList()
    {
        for(int j = 0; j < 36; j++)
        {
            float cumulative = 0f;
            for (int i = 0; i < Events.Count; i++)
            {
                cumulative += EventPercentages[i];
                float rand = Random.Range(0, 100);
                if (cumulative > rand)
                {
                    possibleEvents.Add(Events[i]);
                    break;
                }
            }
        }
        for(int i = 0; i < 12; i++) 
        {
            foreach (Event e in possibleEvents.Where(e => e.minimumSteps <= i))
            {

            }
        }
        
    }
}

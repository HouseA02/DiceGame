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
        public List<StoryData> stories = new List<StoryData>();
    }
    [System.Serializable]
    public class HourList
    {
        public List<Event> events = new List<Event>();
    }
    [SerializeField]
    public EventArenaController eventArenaController;
    public List<Event> events = new List<Event>();
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
    private Sprite questionMark;

    [SerializeField]
    private HourList[] hours;
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
    public void BlindAll()
    {
        foreach(Event e in SpawnedEvents.Where(e => e.id >= lineNum+1))
        {
            BlindEvent(e);
        }
    }

    public void BlindEvent(Event e)
    {
        e.isBlinded = true;
        e.sprites[2].sprite = questionMark;
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
            foreach(HourList h in hours)
            {
                for (int j = 0; j < 3; j++)
                {
                    Event rand = hours[(int)handNum - 1].events[j];
                    Event instance = Instantiate(rand, Points[j].position, Points[j].rotation);
                    instance.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.up);
                    instance.id = (int)handNum;
                    instance.eventArenaController = eventArenaController;
                    instance.tag = handNum.ToString();
                    SpawnedEvents.Add(instance);
                }
                rotateAmount += 30;
                transform.localRotation = Quaternion.Euler(0, rotateAmount, 0);
                handNum++;
            }
            foreach(Event e in SpawnedEvents.Where(e => e.eventType == Event.EventType.Combat))
            {
                e.GetComponent<CombatEvent>().Initialise(acts[act].combats[Random.Range(0, acts[act].combats.Count)]);
            }
            foreach (Event e in SpawnedEvents.Where(e => e.eventType == Event.EventType.Elite))
            {
                e.GetComponent<CombatEvent>().Initialise(acts[act].elites[Random.Range(0, acts[act].elites.Count)]);
            }
            List<StoryData> randStories = new List<StoryData>();
            randStories.AddRange(acts[act].stories);
            randStories = randStories.OrderBy(x => Random.value).ToList();
            foreach (Event e in SpawnedEvents.Where(e => e.eventType == Event.EventType.Story))
            {
                e.GetComponent<StoryEvent>().storyData = randStories[0];
                randStories.Remove(randStories[0]);
                if(randStories.Count < 1)
                {
                    randStories.AddRange(acts[act].stories);
                    randStories = randStories.OrderBy(x => Random.value).ToList();
                }
                //e.GetComponent<StoryEvent>().storyData = acts[act].stories[Random.Range(0, acts[act].stories.Count)];
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
                e.transform.localScale /= 1.5f;
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
            e.transform.localScale *= 1.5f;
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
        for(int i = 0; i < 12; i++) 
        {
            List<Event> validEvents = new List<Event>();
            validEvents.AddRange(events.Where(e => e.minimumSteps <= i));
            List<Event> weightedEvents = new List<Event>();
            foreach (Event e in validEvents)
            {
                for (int j = 0; j <= e.weight; j++)
                {
                    weightedEvents.Add(e);
                }
            }
            for (int j = 0; j < 3; j++)
            {
                hours[i].events.Add(weightedEvents[Random.Range(0, weightedEvents.Count)]);
            }
        }
        
    }
}

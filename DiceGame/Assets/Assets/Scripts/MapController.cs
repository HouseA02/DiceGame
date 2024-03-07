using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> Events = new List<GameObject>();
    public List<float> EventPercentages = new List<float>();

    public float EventsNum;

    public float mapTime;
    public int lineNum;

    private float rotateAmount;
    private float handReset;
    private GameObject clockHand;

    private List<GameObject> SpawnedEvents = new List<GameObject>();
    private List<Transform> Points = new List<Transform>();
    private List<GameObject> currentChoices = new List<GameObject>();
    private List<GameObject> possibleEvents = new List<GameObject>();

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
            foreach (GameObject var in SpawnedEvents)
            {
                Destroy(var);
            }

            float handNum = 1;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    GameObject rand = possibleEvents[Random.Range(0, possibleEvents.Count)];
                    GameObject instance = Instantiate(rand, Points[j].position, Quaternion.identity);
                    possibleEvents.Remove(rand);
                    instance.transform.SetParent(GameObject.Find(instance.tag).transform);
                    instance.tag = handNum.ToString();
                    SpawnedEvents.Add(instance);
                }
                rotateAmount += 30;
                transform.localRotation = Quaternion.Euler(0, rotateAmount, 0);
                handNum++;
            }
            eventChoice();
        }
    }

    public void eventChoice()
    {
        foreach (GameObject var in currentChoices)
        {
            if(var != null)
            {
                var.transform.localScale /= 2;
                var.name = "DeadEvent";
            }
        }
        currentChoices.Clear();
        foreach (GameObject var in GameObject.FindGameObjectsWithTag(mapTime.ToString()))
        {
            currentChoices.Add(var);
        }      
        foreach (GameObject var in currentChoices)
        {
            var.transform.localScale *= 2;
            var.name = "Choose";
        }
        clockHand.transform.RotateAround(transform.position, Vector3.up, 30);
        handReset -= 30;
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
    }
}

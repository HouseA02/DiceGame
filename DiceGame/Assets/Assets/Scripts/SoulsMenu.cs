using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoulsMenu : MonoBehaviour
{
    private GameObject Option1;
    private GameObject Option2;
    private Transform opButton1;
    private Transform opButton2;

    private string buffStat;
    private float buffChange;

    private List<GameObject> Profiles = new List<GameObject>();
    private List<Button> ProfileButtons = new List<Button>();

    private MapController mapController;
    private PlayerController playerController;

    private delegate void MyFunctionDelegate();

    private MyFunctionDelegate[] buffArray;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.Find("Profiles").transform.childCount; i++)
        {
            Profiles.Add(transform.Find("Profiles").transform.GetChild(i).gameObject);
        }
        foreach (GameObject parent in Profiles)
        {
            Transform child = parent.transform.Find("ProfileButton");
            ProfileButtons.Add(child.GetComponent<Button>());
        }

        mapController = FindObjectOfType<MapController>();
        playerController = FindObjectOfType<PlayerController>();

        buffArray = new MyFunctionDelegate[]
        {
            Buff1,
            Buff2,
            Buff3
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerOptions()
    {
        Option1 = Profiles[Random.Range(0, Profiles.Count)];
        Profiles.Remove(Option1);
        Option2 = Profiles[Random.Range(0, Profiles.Count)];
        Profiles.Add(Option1);

        Option1.transform.position = new Vector3(Option1.transform.position.x + 50, Option1.transform.position.y, Option1.transform.position.z);
        opButton1 = Option1.transform.Find("ProfileButton");
        opButton1.gameObject.SetActive(true);
        Option2.transform.position = new Vector3(Option2.transform.position.x + 50, Option2.transform.position.y, Option2.transform.position.z);
        opButton2 = Option2.transform.Find("ProfileButton");
        opButton2.gameObject.SetActive(true);

        int randBuff = Random.Range(0, buffArray.Length);
        buffArray[randBuff]();
        Text buffText = GameObject.Find("BuffText").GetComponent<Text>();
        buffText.text = buffStat + " + " + buffChange;
    }

    public void ActivateBuff()
    {
        string buttonTag = EventSystem.current.currentSelectedGameObject.tag;
        GameObject character = GameObject.FindGameObjectWithTag(buttonTag);
        CharacterController characterController = character.GetComponent<CharacterController>();

        System.Type type = characterController.GetType();

        System.Reflection.FieldInfo buffName = type.GetField(buffStat);
        float value = (float)buffName.GetValue(characterController);
        value += buffChange;

        buffName.SetValue(characterController, value);

        LoopReset();
    }

    public void LoopReset()
    {
        playerController.soulsMenuActive = false;
        Option1.transform.position = new Vector3(Option1.transform.position.x - 50, Option1.transform.position.y, Option1.transform.position.z);
        Option2.transform.position = new Vector3(Option2.transform.position.x - 50, Option2.transform.position.y, Option2.transform.position.z);
        opButton1.gameObject.SetActive(false);
        opButton2.gameObject.SetActive(false);
        mapController.reset = true;
    }

    void Buff1()
    {
        buffStat = "Strength";
        buffChange = 3;
    }
    void Buff2()
    {
        buffStat = "Speed";
        buffChange = 4;
    }
    void Buff3()
    {
        buffStat = "Attack";
        buffChange = 5;
    }
}

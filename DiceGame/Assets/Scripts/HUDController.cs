using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject Compendium;
    public GameObject Achievements;
    [SerializeField]
    private GameObject relicContainer;
    public GameObject mapCam;
    private Animator camMoveAnim;
    bool isTabOpen = false;
    [SerializeField]
    private float tabDistance;
    // Start is called before the first frame update
    void Start()
    {
        camMoveAnim = mapCam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleUITab()
    {
        switch (isTabOpen)
        {
            case true:
                relicContainer.transform.Translate(-tabDistance, 0f, 0f);
                isTabOpen = false;
                break;
            case false:
                relicContainer.transform.Translate(tabDistance, 0f, 0f); 
                isTabOpen = true;
                break;

        }
    }
    public void OpenMap()
    {
        camMoveAnim.SetTrigger("Deactivate");
    }

    public void OpenCompendium()
    {
        Compendium.SetActive(true);
    }

    public void CloseCompendium()
    {
        Compendium.SetActive(false);
    }

    public void OpenAchievemnts()
    {
        Achievements.SetActive(true);
    }

    public void CloseAchievements()
    {
        Achievements.SetActive(false);
    }
}

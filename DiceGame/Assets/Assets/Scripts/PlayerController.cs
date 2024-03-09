using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    public GameObject mapCam;
    private Animator camMoveAnim;

    public float soulCount;

    public GameObject pauseMenu;
    public GameObject pauseMenuSettings;
    private bool isActive;

    public bool soulsMenuActive;

    public LayerMask layerMask;

    private SoulsMenu soulsMenu;
    private MapController mapController;
    private EventArenaController eventArenaController;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        soulsMenu = FindObjectOfType<SoulsMenu>();
        mapController = FindObjectOfType<MapController>();
        eventArenaController = FindObjectOfType<EventArenaController>();
        //camMoveAnim = mapCam.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* OLD
        if (Input.GetMouseButtonDown(0) && !gameManager.inBattle && false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                if (!soulsMenuActive)
                {
                    if (hit.transform.name == "Choose" && hit.transform.tag != "12")
                    {
                        mapController.line.positionCount++;
                        mapController.mapTime++;
                        mapController.eventChoice();
                        mapController.line.SetPosition(mapController.lineNum, hit.transform.position);
                        mapController.lineNum++;

                        eventArenaController.eventName = hit.transform.parent.name;
                        //Renderer hitRenderer = hit.transform.GetChild(0).GetComponent<Renderer>();
                        //hitRenderer.material.SetColor("_Color", Color.white);
                    }
                    if(hit.transform.tag == "12")
                    {
                        //soulsMenu.PlayerOptions();
                        // soulsMenuActive = true;
                        mapController.reset = true;
                    }
                }
            }
        }*/

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ActivatePauseMenu();
        }
    }

    public void ActivatePauseMenu()
    {
        soulsMenuActive = !soulsMenuActive;
        isActive = !isActive;
        pauseMenu.SetActive(isActive);
        pauseMenuSettings.SetActive(false);
    }
}

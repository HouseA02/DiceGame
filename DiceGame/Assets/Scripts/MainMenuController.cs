using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject SettingsMenuActive;
    public GameObject pauseMenu;
    public float fadeTime;

    private PlayerController playerController;
    private bool deactivate;
    private bool isActive;

    private CanvasGroup canvasAlpha;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.soulsMenuActive = true;
        isActive = false;
        canvasAlpha = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deactivate)
        {
            canvasAlpha.alpha -= fadeTime * Time.deltaTime;
            playerController.soulsMenuActive = false;
        }
        if(canvasAlpha.alpha <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        deactivate = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        isActive = !isActive;
        SettingsMenuActive.SetActive(isActive);
    }

    public void MainMenu()
    {
        deactivate = false;
        pauseMenu.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown mainMenuResolutionDropdown;

    [SerializeField]
    private GameObject[] tabs;

    private bool isFullscreen;

    private List<string> resolutionList = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;

        foreach (var resolution in resolutions) 
        {
            resolutionList.Add(resolution.width + " x " + resolution.height + " @ " + resolution.refreshRateRatio + "Hz");
        }
        resolutionDropdown.AddOptions(resolutionList);
        //mainMenuResolutionDropdown.AddOptions(resolutionList);

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
        QualitySettings.vSyncCount = 1;
        QualitySettings.SetQualityLevel(3);
    }

    public void SettingsMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(1920, 1080, isFullscreen);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        isFullscreen = !isFullscreen;
    }

    public void SetVSync()
    {
        float isActive = 1;
        if (isActive == 1)
        {
            QualitySettings.vSyncCount = 1;
        }
        else if(isActive == 0)
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void TabController(int tabNum)
    {
        foreach(var tab in tabs)
        {
            tab.SetActive(false);
        }
        tabs[tabNum].SetActive(true);
    }
}

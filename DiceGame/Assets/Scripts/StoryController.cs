using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject storyPanel;
    [SerializeField]
    private TMP_Text nameSlot;
    [SerializeField]
    private TMP_Text descriptionSlot;
    [SerializeField]
    private Image artSlot;
    private StoryData currentStory;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private TMP_Text[] buttonText;

    public void StartStory(StoryData storyData)
    {
        storyPanel.SetActive(true);
        currentStory = storyData;
        nameSlot.text = currentStory.storyName;
        descriptionSlot.text = currentStory.storyDescription;
        if (currentStory.storyArt != null) { artSlot.sprite = currentStory.storyArt; }
        if(storyData.storyOptions != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].gameObject.SetActive(false);
            }
            for(int i = 0;i < currentStory.storyOptions.Options.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttonText[i].text = currentStory.storyOptions.Options[i].text;
                buttons[i].onClick.AddListener(storyData.storyOptions.Options[i].Activate);
            }
        }
    }
}

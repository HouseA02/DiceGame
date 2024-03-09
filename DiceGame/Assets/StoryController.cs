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

    public void StartStory(StoryData storyData)
    {
        storyPanel.SetActive(true);
        currentStory = storyData;
        nameSlot.text = currentStory.storyName;
        descriptionSlot.text = currentStory.storyDescription;
        if (currentStory.storyArt != null) { artSlot.sprite = currentStory.storyArt; }
    }
}

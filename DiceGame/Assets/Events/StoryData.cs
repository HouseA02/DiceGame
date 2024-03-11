using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "ScriptableObjects/Story", order = 2)]
public class StoryData : ScriptableObject
{
    [SerializeField]
    public string storyName;
    [SerializeField]
    public string storyDescription;
    [SerializeField]
    public Sprite storyArt;
    [SerializeField]
    public StoryOptions storyOptions;
}


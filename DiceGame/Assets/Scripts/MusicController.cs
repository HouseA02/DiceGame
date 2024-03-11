using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    public void NewTrack(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}

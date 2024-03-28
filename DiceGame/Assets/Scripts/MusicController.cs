using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource[] audioSFX;

    public void NewTrack(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void OnMasterChange(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    public void OnMusicChange(Slider slider)
    {
        musicSource.volume = slider.value;
    }

    public void OnSFXChange(Slider slider)
    {
/*        foreach(var audio in audioSFX)
        {
            audio.volume = slider.value;
        }*/
    }

    public void OnGUIChange(Slider slider)
    {
        AudioListener.volume = slider.value;
    }
}

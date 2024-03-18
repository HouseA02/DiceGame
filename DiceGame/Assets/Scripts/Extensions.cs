using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Extensions : MonoBehaviour
{

}
public static class IEnumerableExtensions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
       => self.Select((item, index) => (item, index));
}
public static class AudioExtensions
{
    public static void PlayOneShot(this AudioSource audioSource, AudioClip audioClip, float volumeScale, float pitchScale)
    {
        audioSource.pitch = pitchScale;
        audioSource.PlayOneShot(audioClip, volumeScale);    
    }
}

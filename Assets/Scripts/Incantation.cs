using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Incantation : MonoBehaviour
{
    public AudioClip   incantationMainSound;
    public AudioClip   incantationEndSound;
    public AudioClip   incantationLevelUpSound;
    public AudioSource audio;

    public void PlayIncantation()
    {
        audio.Stop();
        audio.PlayOneShot(incantationMainSound, 1f);        
    }

    public void StopIncantation(bool success)
    {
        audio.Stop();
        if (success)
            audio.PlayOneShot(incantationLevelUpSound, 0.5f);
        else
            audio.PlayOneShot(incantationEndSound, 0.5f);
    }
}
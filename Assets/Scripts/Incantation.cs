using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Incantation : MonoBehaviour
{
    private AudioClip   incantationMainSound;
    private AudioClip   incantationEndSound;
    private AudioSource audio;

    private void Awake()
    {
        incantationMainSound = (AudioClip)Resources.Load("Sound/incantation", typeof(AudioClip));
        incantationEndSound = (AudioClip)Resources.Load("Sound/incantation_end", typeof(AudioClip));
        audio = GetComponent<AudioSource>();
    }

    public void PlayIncantation()
    {
        audio.Stop();
        audio.PlayOneShot(incantationMainSound, 1f);        
    }

    public void StopIncantation(bool success)
    {
        audio.Stop();
        if (success)
            audio.PlayOneShot(incantationEndSound, 0.5f);        
        else
        {
            // todo
        }
    }
}
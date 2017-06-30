using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]
public class Incantation : MonoBehaviour
{
    // Audio
    public AudioClip   incantationMainSound;
    public AudioClip   incantationEndSound;
    public AudioClip   incantationLevelUpSound;
    public AudioSource audio;

    // Particle
    public ParticleSystem particle;

    public void PlayIncantation()
    {
        audio.Stop();
        audio.PlayOneShot(incantationMainSound, 1f);
        particle.Stop();
        particle.Play();
        StopIncantation(true);  
    }

    public void StopIncantation(bool success)
    {
        audio.Stop();
        if (success)
        {
            particle.Stop();
            audio.PlayOneShot(incantationLevelUpSound, 0.5f);
        }
        else
        {
            particle.Stop();
            audio.PlayOneShot(incantationEndSound, 0.5f);
        }
    }
}
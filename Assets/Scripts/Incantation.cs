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

    public void PlayIncantation(Vector3 playerPos)
    {
        audio.Stop();
        audio.loop = true;
        audio.clip = incantationMainSound;
        audio.Play();
        particle.Stop();
        particle.transform.position = playerPos;
        particle.Play();
    }

    public void StopIncantation(bool success)
    {
        audio.loop = false;
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
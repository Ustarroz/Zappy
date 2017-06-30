using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Egg : MonoBehaviour
{
    // Audio
    public AudioClip   eggPopSound;
    public AudioClip   eggCrackSound;
    public AudioSource audio;

    public void Pop(Vector3 playerPos)
    {
        audio.Stop();
        audio.PlayOneShot(eggPopSound, 1);
    }

    public void Crack()
    {
        audio.Stop();
        audio.PlayOneShot(eggCrackSound, 1);
    }
}
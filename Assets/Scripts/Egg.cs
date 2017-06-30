using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Egg : MonoBehaviour
{
    private Vector2 position;
    private int     id;

    // Audio
    public float       volume;
    public AudioClip   eggPopSound;
    public AudioClip   eggCrackSound;
    public AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Pop()
    {
        audio.Stop();
        audio.PlayOneShot(eggPopSound, volume);
    }

    public void Crack()
    {
        audio.Stop();
        audio.PlayOneShot(eggCrackSound, volume);
    }

    public Vector2 getPosition()
    {
        return position;
    }

    public void setPosition(Vector2 _pos)
    {
        position = _pos;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int _id)
    {
        id = _id;
    }
}
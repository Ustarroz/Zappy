using UnityEngine;
using System.Collections;

public class Incantation : MonoBehaviour
{
    // Position
    private Vector2 position;

    // Audio
    private  AudioClip   incantationMainSound;
    private  AudioClip   incantationEndSound;
    private  AudioClip   incantationLevelUpSound;
    private  AudioSource audio;

    // Particle
    public ParticleSystem particle;

    // Id of incant
    private static int nextid = 0;
    private int        id;

    private void Awake()
    {
        id = nextid;
        nextid++;
        audio = GetComponent<AudioSource>();
        incantationMainSound    = (AudioClip)Resources.Load("incantation");
        incantationEndSound     = (AudioClip)Resources.Load("incantation_end");
        incantationLevelUpSound = (AudioClip)Resources.Load("levelup");
    }

    public void PlayIncantation()
    {
        Vector3 pos = new Vector3(position.x, position.y, 0);

        audio.Stop();
        audio.loop = true;
        audio.clip = incantationMainSound;
        audio.Play();
        particle.Stop();
        particle.transform.position = pos;
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

    public void setPosition(Vector2 pos)
    {
        position = pos;
    }

    public Vector2 getPosition()
    {
        return position;
    }

    public int getId()
    {
        return id;
    }

}
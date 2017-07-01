using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Incantation : MonoBehaviour
{
    // Position
    private Vector2 position = new Vector2(0, 0);

    // Audio
    public float volume;
    public  AudioClip   incantationMainSound;
    public  AudioClip   incantationEndSound;
    public  AudioClip   incantationLevelUpSound;
    public  AudioSource audio;

    // Particle
    public ParticleSystem particle;

    // Id of incant
    private static int nextid = 0;
    private int        id;

    private void Awake()
    {
        particle.Stop();
        id = nextid;
        nextid++;
        audio = GetComponent<AudioSource>();
/*        incantationMainSound    = (AudioClip)Resources.Load("incantation.ogg");
        incantationEndSound     = (AudioClip)Resources.Load("incantation_end.ogg");
        incantationLevelUpSound = (AudioClip)Resources.Load("levelup.ogg");
*/
    }

    public void PlayIncantation()
    {
        Vector3 pos = new Vector3(4, 0, 0);

        audio.Stop();
        audio.loop = true;
        audio.clip = incantationMainSound;
        audio.volume = volume;
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
            audio.PlayOneShot(incantationLevelUpSound, volume);
        }
        else
        {
            particle.Stop();
            audio.PlayOneShot(incantationEndSound, volume);
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
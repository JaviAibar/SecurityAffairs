using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour {
    public AudioSource lightning;
    public AudioSource clock;
    public AudioSource crack;
    public void PlaySoundLightning()
    {
        lightning.Play();
    }
    public void StopSoundLightning()
    {
        lightning.Stop();
    }
    public void PlaySoundClock()
    {
        clock.Play();
    }
    public void PlaySoundCrack()
    {
        crack.Play();
    }
}

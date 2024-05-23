using UnityEngine;

public class PlaySceneSoundScript : MonoBehaviour
{
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

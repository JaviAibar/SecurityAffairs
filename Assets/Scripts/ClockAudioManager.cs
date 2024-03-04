using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _thunderAudioSource;
    [SerializeField] private AudioSource _clockAudioSource;

    public void PlayClock()
    {
        _clockAudioSource.Play();
    }

    public void PlayThunder()
    {
        _thunderAudioSource.Play();
    }

    public void StopThunder()
    {
        _thunderAudioSource.Stop();
    }
}

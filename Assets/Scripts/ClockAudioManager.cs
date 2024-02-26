using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_ThunderAudioSource;
    [SerializeField] private AudioSource m_ClockAudioSource;

    public void PlayClock()
    {
        m_ClockAudioSource.Play();
    }

    public void PlayThunder()
    {
        m_ThunderAudioSource.Play();
    }
}

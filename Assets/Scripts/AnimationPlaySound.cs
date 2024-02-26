using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlaySound : MonoBehaviour
{
    private AudioSource m_AudioSource;
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        m_AudioSource.Play();
    }
}

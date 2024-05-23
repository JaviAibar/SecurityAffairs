using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundScript : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    private AudioSource _audio;

    public void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = _sound;
    }

    public void PlaySound()
    {
        _audio.pitch = Random.Range(0.6f, 0.8f);
        _audio.Play();
    }
}

using UnityEngine;
using Zenject;

public class AudioService : IAudioService
{
    private AudioSource _clockAudioSource;

    [Inject]
    public void Construct(AudioSource clockAudioSource)
    {
        this._clockAudioSource = clockAudioSource;
    }

    public void StartPlaying()
    {
        _clockAudioSource.Play();
    }
}


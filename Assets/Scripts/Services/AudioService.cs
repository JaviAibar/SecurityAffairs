using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace SecurityAffairs.Scripts.Services
{
    internal class AudioService : IAudioService
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
}

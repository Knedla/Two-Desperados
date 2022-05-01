using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.IO.Audio
{
    public class OneShotAudioSourceController : AudioSourceController
    {
        public float MinPitch = 0.8f;
        public float MaxPitch = 1.2f;

        public List<AudioClip> AudioClips;

        bool isSingleAudioClip;
        AudioClip audioClip;

        bool randomizePitch;

        private void Awake()
        {
            isSingleAudioClip = AudioClips.Count == 1;

            if (isSingleAudioClip)
                audioClip = AudioClips[0];

            randomizePitch = MinPitch != 1 || MaxPitch != 1;
        }

        public override void Play()
        {
            if (randomizePitch)
                AudioSource.pitch = Random.Range(MinPitch, MaxPitch);

            if (isSingleAudioClip)
                AudioSource.PlayOneShot(audioClip);
            else
                AudioSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Count)]);
        }

        public override void Stop() { }
    }
}
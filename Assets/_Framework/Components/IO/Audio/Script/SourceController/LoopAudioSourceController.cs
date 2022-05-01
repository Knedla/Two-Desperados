using System.Collections;
using UnityEngine;

namespace Game.Components.IO.Audio
{
    public class LoopAudioSourceController : AudioSourceController
    {
        public float MaxVolume = 0.35f;
        public float FadeInModifier = 2.5f;
        public float FadeOutModifier = 2.5f;

        public override void Play()
        {
            StartCoroutine(ChangeGarbageDisposalVolume(true));
        }

        public override void Stop()
        {
            StartCoroutine(ChangeGarbageDisposalVolume(false));
        }

        IEnumerator ChangeGarbageDisposalVolume(bool volumeUp)
        {
            if (volumeUp)
            {
                AudioSource.volume = 0;
                AudioSource.Play();

                while (AudioSource.volume < MaxVolume)
                {
                    AudioSource.volume += Time.deltaTime * FadeInModifier;

                    if (AudioSource.volume > MaxVolume)
                        AudioSource.volume = MaxVolume;

                    yield return null;
                }
            }
            else
            {
                while (AudioSource.volume > 0)
                {
                    AudioSource.volume -= Time.deltaTime * FadeOutModifier;
                    yield return null;
                }

                AudioSource.Stop();
            }
        }
    }
}
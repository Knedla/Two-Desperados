using UnityEngine;
namespace Game.Components.IO.Audio
{
    public abstract class AudioSourceController : MonoBehaviour
    {
        public AudioSource AudioSource;

        public abstract void Play();

        public abstract void Stop();
    }
}
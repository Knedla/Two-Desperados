using UnityEngine;
using UnityEngine.Audio;

namespace Game.Components.IO.Audio
{
    public class InitializeManager : MonoBehaviour
    {
        public AudioMixer MasterMixer;

        void Awake() // stavi Start ako puca
        {
            Framework.AudioManager.SetAudioMixer(MasterMixer);
        }
    }
}
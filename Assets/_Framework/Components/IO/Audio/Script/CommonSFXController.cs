using UnityEngine;

namespace Game.Components.IO.Audio
{
    public class CommonSFXController : MonoBehaviour
    {
        public AudioSource AudioSource;

        public AudioClip ButonClick;

        void Awake()
        {
            AudioSource.ignoreListenerPause = true;
            Framework.EventManager.StartListening(System.Event.SystemListener.OnButtonClick, PlayButonClick);
        }

        private void OnDestroy()
        {
            Framework.EventManager.StopListening(System.Event.SystemListener.OnButtonClick, PlayButonClick);
        }

        public void PlayButonClick()
        {
            AudioSource.PlayOneShot(ButonClick);
        }
    }
}

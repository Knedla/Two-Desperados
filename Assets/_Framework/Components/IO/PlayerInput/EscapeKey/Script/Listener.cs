using UnityEngine;

namespace Game.Components.IO.PlayerInput.EscapeKey
{
    public class Listener : MonoBehaviour, IListener
    {
        static Listener instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            Framework.EscapeKeyManager.SetListener(this);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Framework.EscapeKeyManager.Pressed();
        }
    }
}

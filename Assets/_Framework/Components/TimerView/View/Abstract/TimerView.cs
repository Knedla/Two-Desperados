using UnityEngine;

namespace Game.Components.Timer
{
    public abstract class TimerView : MonoBehaviour
    {
        private void Awake()
        {
            Hide();
        }

        public abstract void SetData(float time);

        public abstract void UpdateView(float timePass);

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

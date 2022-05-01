using Game.System.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.Experience
{
    public class ExperienceView : MonoBehaviour
    {
        public Text Text;

        public void OnEnable()
        {
            Framework.EventManager.StartListening(CustomListener.OnExperienceQuantityChange, SetText);
        }

        private void OnDisable()
        {
            Framework.EventManager.StopListening(CustomListener.OnExperienceQuantityChange, SetText);
        }

        void SetText()
        {
            Text.text = Framework.PlayerData.Experience.Value.ToString();
        }

        private void Awake()
        {
            SetText();
        }
    }
}

using UnityEngine.UI;

namespace Game.Components.UI
{
    public class CustomButton : Button
    {
        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() => Framework.EventManager.TriggerEvent(System.Event.SystemListener.OnButtonClick));
        }
    }
}

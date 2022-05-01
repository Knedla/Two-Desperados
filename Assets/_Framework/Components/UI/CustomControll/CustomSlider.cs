using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class CustomSlider : Slider, IPointerUpHandler, IEndDragHandler
    {
        public UnityAction OnEndDragAction;
        public UnityAction OnPointerUpAction;

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (OnEndDragAction != null)
                OnEndDragAction.Invoke();

            Framework.EventManager.TriggerEvent(System.Event.SystemListener.OnButtonClick);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (!interactable)
                return;

            if (OnPointerUpAction != null)
                OnPointerUpAction.Invoke();

            Framework.EventManager.TriggerEvent(System.Event.SystemListener.OnButtonClick);
        }
    }
}
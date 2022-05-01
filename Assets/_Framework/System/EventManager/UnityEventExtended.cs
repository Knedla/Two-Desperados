using UnityEngine.Events;

namespace Game.System.Event
{
    public class UnityEventExtended : UnityEvent
    {
        int listenerCount;
        public int ListenerCount { get { return listenerCount; } }

        new public void AddListener(UnityAction call)
        {
            base.AddListener(call);
            listenerCount++;
        }

        new public void RemoveListener(UnityAction call)
        {
            base.RemoveListener(call);
            listenerCount--;
        }
    }
}
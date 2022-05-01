using UnityEngine.Events;
using System.Collections.Generic;

namespace Game.System.Event
{
    public class Manager : IManager
    {
        public object TriggeringObject { get; set; }

        Dictionary<string, UnityEventExtended> events;

        public Manager()
        {
            events = new Dictionary<string, UnityEventExtended>();
        }

        public void StartListening(SystemListener eventName, UnityAction listener)
        {
            StartListening(eventName.ToString(), listener);
        }

        public void StartListening(CustomListener eventName, UnityAction listener)
        {
            StartListening(eventName.ToString(), listener);
        }

        public void StartListening(string eventName, UnityAction listener)
        {
            UnityEventExtended thisEvent;
            if (events.TryGetValue(eventName, out thisEvent))
                thisEvent.AddListener(listener);
            else
            {
                thisEvent = new UnityEventExtended();
                thisEvent.AddListener(listener);
                events.Add(eventName, thisEvent);
            }
        }

        public void StopListening(SystemListener eventName, UnityAction listener)
        {
            StopListening(eventName.ToString(), listener);
        }

        public void StopListening(CustomListener eventName, UnityAction listener)
        {
            StopListening(eventName.ToString(), listener);
        }

        public void StopListening(string eventName, UnityAction listener)
        {
            UnityEventExtended thisEvent;
            if (events.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);

                if (thisEvent.ListenerCount == 0)
                    events.Remove(eventName);
            }
        }

        public void TriggerEvent(SystemListener eventName)
        {
            TriggerEvent(eventName.ToString(), null);
        }

        public void TriggerEvent(CustomListener eventName)
        {
            TriggerEvent(eventName.ToString(), null);
        }

        public void TriggerEvent(string eventName)
        {
            TriggerEvent(eventName, null);
        }

        public void TriggerEvent(CustomListener eventName, object triggerinObject)
        {
            TriggerEvent(eventName.ToString(), triggerinObject);
        }

        public void TriggerEvent(string eventName, object triggerinObject)
        {
            UnityEventExtended thisEvent;
            if (events.TryGetValue(eventName, out thisEvent))
            {
                TriggeringObject = triggerinObject;
                thisEvent.Invoke();
                TriggeringObject = null;
            }
        }

        public void RemoveAllListeners(SystemListener eventName)
        {
            RemoveAllListeners(eventName.ToString());
        }

        public void RemoveAllListeners(CustomListener eventName)
        {
            RemoveAllListeners(eventName.ToString());
        }

        public void RemoveAllListeners(string eventName)
        {
            UnityEventExtended thisEvent;
            if (events.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveAllListeners();
                events.Remove(eventName);
            }
        }
    }
}
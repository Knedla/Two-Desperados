using UnityEngine.Events;

namespace Game.System.Event
{
    public interface IManager
    {
        object TriggeringObject { get; set; }

        void StartListening(SystemListener eventName, UnityAction listener);
        void StartListening(CustomListener eventName, UnityAction listener);
        void StartListening(string eventName, UnityAction listener);

        void StopListening(SystemListener eventName, UnityAction listener);
        void StopListening(CustomListener eventName, UnityAction listener);
        void StopListening(string eventName, UnityAction listener);

        void TriggerEvent(SystemListener eventName);
        void TriggerEvent(CustomListener eventName);
        void TriggerEvent(string eventName);

        void TriggerEvent(CustomListener eventName, object triggerinObject); //hack resenje da dodjem do objekta koji je okinuo trigger; treba prebaciti UnityAction listener u UnityAction<object> listener
        void TriggerEvent(string eventName, object triggerinObject);

        void RemoveAllListeners(SystemListener eventName);
        void RemoveAllListeners(CustomListener eventName);
        void RemoveAllListeners(string eventName);
    }
}

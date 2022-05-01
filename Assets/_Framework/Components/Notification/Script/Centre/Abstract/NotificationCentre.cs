using UnityEngine;

namespace Game.Components.Notification
{
    public abstract class NotificationCentre<T, U> : MonoBehaviour, ICentre where T : IItem<U> where U : class, INotification
    {
        public T Item;

        public abstract void Notify(INotification notification);

        bool registered; // moze ovo pametije da se napravi

        protected virtual void Awake()
        {
            registered = Framework.NotificationManager.Register(typeof(U), this);

            if (!registered)
                Destroy(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (registered)
                Framework.NotificationManager.Unregister(typeof(U));
        }
    }
}
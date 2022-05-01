using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.Components.Notification
{
    public class InstantNotificationCentre : NotificationCentre<InstantNotificationItem, InstantNotification>
    {
        List<InstantNotificationItem> items;

        bool waiting;

        protected override void Awake()
        {
            items = new List<InstantNotificationItem>();

            Framework.PoolManager.Register(gameObject, Item, 0);

            base.Awake();
        }

        public override void Notify(INotification notification)
        {
            if (notification.Id != -1 && items.Any(s => s.Id == notification.Id))
                return;

            InstantNotificationItem notificationItem = Framework.PoolManager.Dequeue<InstantNotificationItem>(gameObject);
            notificationItem.Initialize((InstantNotification)notification);
            notificationItem.transform.localPosition = new UnityEngine.Vector3(notificationItem.transform.localPosition.x, notificationItem.transform.localPosition.y, 0);

            items.Add(notificationItem);

            if (!waiting)
                StartCoroutine(WaitToFinish());
        }

        IEnumerator WaitToFinish()
        {
            waiting = true;

            while (items.Exists(s => !s.Finished))
                yield return null;

            items.Clear();
            Framework.PoolManager.EnqueueAll(gameObject);

            waiting = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Framework.PoolManager.Unregister<InstantNotificationItem>(gameObject);
        }
    }
}
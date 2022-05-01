using System;
using System.Collections.Generic;

namespace Game.Components.Notification
{
    public class Manager : IManager
    {
        Dictionary<Type, ICentre> centres;

        public Manager()
        {
            centres = new Dictionary<Type, ICentre>();
        }

        public bool Register(Type notificationType, ICentre centre) // podsetnik: da se ne prosledjuje type nego da se to izvuce iz INotificationCentre ili tako nekako
        {
            if (centres.ContainsKey(notificationType))
                return false;

            centres.Add(notificationType, centre);

            return true;
        }

        public void Unregister(Type notificationType)
        {
            if (centres.ContainsKey(notificationType))
                centres.Remove(notificationType);
        }

        public void Notify(INotification notification)
        {
            ICentre centre;
            if (centres.TryGetValue(notification.GetType(), out centre))
                centre.Notify(notification);
        }
    }
}

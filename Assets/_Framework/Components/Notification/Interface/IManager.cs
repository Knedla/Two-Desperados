using System;

namespace Game.Components.Notification
{
    public interface IManager
    {
        bool Register(Type notificationType, ICentre centre);
        void Unregister(Type notificationType);

        void Notify(INotification notification);
    }
}

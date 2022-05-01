namespace Game.Components.Notification
{
    public interface IItem<T> where T : INotification
    {
        void Initialize(T notification);
    }
}

namespace Game.Components.IO.PlayerInput.EscapeKey
{
    public interface IManager
    {
        void SetListener(IListener listener);
        
        bool IsBlocked { get; }
        void Block();
        void Unblock();

        void Register(IItem item);
        void Unregister(IItem item);

        void Pressed();
    }
}
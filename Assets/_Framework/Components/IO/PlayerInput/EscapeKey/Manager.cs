using System.Collections.Generic;
using System.Linq;

namespace Game.Components.IO.PlayerInput.EscapeKey
{
    public class Manager : IManager
    {
        public bool IsBlocked { get; private set; }

        IListener listener;

        List<IItem> items;

        public Manager()
        {
            items = new List<IItem>();
        }

        public void SetListener(IListener listener)
        {
            this.listener = listener;
            this.listener.Disable();
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void Unblock()
        {
            IsBlocked = false;
        }

        public void Register(IItem item)
        {
            items.Add(item);

            if (items.Count == 1)
                listener.Enable();
        }

        public void Unregister(IItem item)
        {
            if (!items.Contains(item))
                return;

            items.Remove(item);

            if (items.Count == 0)
                listener.Disable();
        }

        public void Pressed()
        {
            if (IsBlocked)
                return;

            if (items.Count > 0)
                items.Last().EscapeKeyPressed();
        }
    }
}
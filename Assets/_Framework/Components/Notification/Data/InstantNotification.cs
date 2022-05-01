using UnityEngine;

namespace Game.Components.Notification
{
    public class InstantNotification : INotification
    {
        public int Id { get; }
        public Sprite Sprite { get; }
        public string Title { get; }
        public string Description { get; }

        public InstantNotification(Sprite sprite, string title, string description, int id = -1)
        {
            Id = id;
            Sprite = sprite;
            Title = title;
            Description = description;
        }
    }
}
using UnityEngine;

namespace Game.Components.IO.PlayerInput.EscapeKey
{
    public abstract class Item : MonoBehaviour, IItem
    {
        protected virtual void OnEnable()
        {
            Framework.EscapeKeyManager.Register(this);
        }

        protected virtual void OnDisable()
        {
            Framework.EscapeKeyManager.Unregister(this);
        }

        public abstract void EscapeKeyPressed();
    }
}
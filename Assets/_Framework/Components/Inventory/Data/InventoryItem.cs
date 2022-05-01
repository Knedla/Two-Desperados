using System;

namespace Game.Components.InventorySystem
{
    [Serializable]
    public class InventoryItem : IInventoryItem
    {
        int quantity;
        public int Quantity { get { return quantity; } }

        public InventoryItem(int quantity = 1)
        {
            this.quantity = quantity;
        }

        public void Add(int quantity = 1)
        {
            this.quantity += quantity;
        }

        public void Remove(int quantity = 1)
        {
            this.quantity -= quantity;
        }
    }
}
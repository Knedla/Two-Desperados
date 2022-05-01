using Game.System.Entity;
using System;
using System.Collections.Generic;

namespace Game.Components.InventorySystem
{
    [Serializable]
    public class Inventory : IInventory
    {
        public void OnDataChange()
        {
            Framework.PlayerData.OnDataChange();
        }

        Dictionary<int, IInventoryItem> items;

        public int GetQuantity(int itemId)
        {
            IInventoryItem inventoryItem;

            if (items.TryGetValue(itemId, out inventoryItem))
                return inventoryItem.Quantity;
            else
                return 0;
        }

        public void Add(int itemId, int count = 1)
        {
            IInventoryItem inventoryItem;

            if (items.TryGetValue(itemId, out inventoryItem))
                inventoryItem.Add(count);
            else
                items.Add(itemId, new InventoryItem(count));

            OnDataChange();
        }

        public void RemoveQuantity(int itemId, int count = 1)
        {
            IInventoryItem inventoryItem;

            if (items.TryGetValue(itemId, out inventoryItem))
            {
                inventoryItem.Remove(count);

                if (inventoryItem.Quantity == 0)
                    items.Remove(itemId);

                OnDataChange();
            }
        }

        public bool TryRemoveQuantity(int itemId, int count = 1)
        {
            IInventoryItem inventoryItem;

            if (!items.TryGetValue(itemId, out inventoryItem))
                return false;

            if (inventoryItem.Quantity < count)
                return false;

            inventoryItem.Remove(count);

            if (inventoryItem.Quantity == 0)
                items.Remove(itemId);

            OnDataChange();

            return true;
        }

        public void RemoveItem(int itemId)
        {
            if (!items.ContainsKey(itemId))
                return;

            items.Remove(itemId);

            OnDataChange();
        }

        public void SetDefaultValues()
        {
            items = new Dictionary<int, IInventoryItem>();
            items.Add(new Trap().Id, new InventoryItem(20));
            items.Add(new Nuke().Id, new InventoryItem(20));
        }

        public void LocalSave()
        {
            Framework.PlayerData.LocalSave();
        }

        public void UploadToCloud()
        {
            Framework.PlayerData.UploadToCloud();
        }
    }
}
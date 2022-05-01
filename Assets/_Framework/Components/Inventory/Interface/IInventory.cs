using Game.System.PersistentData;

namespace Game.Components.InventorySystem
{
    public interface IInventory : IPersistentData
    {
        int GetQuantity(int itemId);
        void Add(int itemId, int count = 1);
        void RemoveQuantity(int itemId, int count = 1);
        bool TryRemoveQuantity(int itemId, int count = 1);
        void RemoveItem(int itemId);
    }
}

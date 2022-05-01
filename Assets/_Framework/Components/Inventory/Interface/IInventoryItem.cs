namespace Game.Components.InventorySystem
{
    public interface IInventoryItem
    {
        int Quantity { get; }

        void Add(int quantity = 1);
        void Remove(int quantity = 1);
    }
}

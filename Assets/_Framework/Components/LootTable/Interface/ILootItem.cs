namespace Game.Components.LootTable
{
    public interface ILootItem
    {
        int DropChance { get; }
        int GetQuantity();
        void CollectItem();
    }
}
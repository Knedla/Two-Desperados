namespace Game.Components.LootTable
{
    public interface IManager
    {
        ILootItem GetLootItem(int lootTableId);
        ILootItem GetLootItem(int lootTableId, int dropNothingChanceModifier);
    }
}

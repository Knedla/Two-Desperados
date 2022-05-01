namespace Game.Components.LootTable
{
    public interface ILootTable
    {
        ILootItem GetLootItem();
        ILootItem GetLootItem(int dropNothingChanceModifier);
    }
}

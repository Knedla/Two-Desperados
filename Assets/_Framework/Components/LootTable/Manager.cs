using Game.System.LazyLoader;

namespace Game.Components.LootTable
{
    //nema unload
    public class Manager : LazyLoaderWithPool<Database, ILootTable>, IManager
    {
        public ILootItem GetLootItem(int lootTableId)
        {
            ILootTable lootTable = GetItem(lootTableId);

            if (lootTable != null)
                return lootTable.GetLootItem();

            return null;
        }

        public ILootItem GetLootItem(int lootTableId, int dropNothingChanceModifier)
        {
            ILootTable lootTable = GetItem(lootTableId);

            if (lootTable != null)
                return lootTable.GetLootItem(dropNothingChanceModifier);

            return null;
        }

        protected override ILootTable LoadItem(int id)
        {
            switch (id)
            {
                case (int)LootTableId.TreasureLootTable: return pool.GetTreasureLootTable();

                default: return null;
            }
        }
    }
}
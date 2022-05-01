using Game.System.Entity;
using System.Collections.Generic;

namespace Game.Components.LootTable
{
    public class Database
    {
        //ne podrzava iteme sa generisanim vrednostima GrateAxe => Dmg = Random.Range(1, 3), podrzava samo stakabilne iteme
        public ILootTable GetTreasureLootTable()
        {
            List<ILootItem> lootItems = new List<ILootItem>();
            lootItems.Add(new ItemLootItem(new Nuke(), 1));
            lootItems.Add(new ItemLootItem(new Trap(), 1));

            return new ChanceLootTable(1, lootItems);
        }
    }
}
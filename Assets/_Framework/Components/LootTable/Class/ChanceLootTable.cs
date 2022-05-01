using System.Collections.Generic;
using UnityEngine;

namespace Game.Components.LootTable
{
    public class ChanceLootTable : ILootTable
    {
        List<ILootItem> lootItems;
        int dropChanceSum;

        public ChanceLootTable(int dropNothingChance, List<ILootItem> lootItems)
        {
            dropChanceSum = dropNothingChance;
            this.lootItems = lootItems;

            for (int i = 0; i < lootItems.Count; i++)
                dropChanceSum += lootItems[i].DropChance;
        }

        public ILootItem GetLootItem()
        {
            int randomValue = Random.Range(1, dropChanceSum + 1);

            int dropChance = 0;

            foreach (ILootItem item in lootItems)
            {
                dropChance += item.DropChance;

                if (randomValue <= dropChance)
                    return item;
            }

            return null;
        }

        public ILootItem GetLootItem(int dropNothingChanceModifier)
        {
            if (dropNothingChanceModifier < 0) //prebudzono, sam property je bzvz i jos ovo sa < 0 je jos bezveznije - moze to pametnije
                return null;

            int randomValue = Random.Range(1, (dropChanceSum * dropNothingChanceModifier) + 1);

            int dropChance = 0;

            foreach (ILootItem item in lootItems)
            {
                dropChance += item.DropChance;

                if (randomValue <= dropChance)
                    return item;
            }

            return null;
        }
    }
}
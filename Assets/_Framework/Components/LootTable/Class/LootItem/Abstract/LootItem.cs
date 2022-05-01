using UnityEngine;

namespace Game.Components.LootTable
{
    public abstract class LootItem : ILootItem
    {
        int dropChance;
        public int DropChance { get { return dropChance; } }

        int minQuantity;
        int maxQuantity;

        public LootItem(int dropChance, int minQuantity = 1, int maxQuantity = 1)
        {
            this.dropChance = dropChance;
            this.minQuantity = minQuantity;
            this.maxQuantity = maxQuantity;
        }

        public int GetQuantity()
        {
            if (minQuantity == maxQuantity)
                return minQuantity;
            else
                return Random.Range(minQuantity, maxQuantity + 1);
        }

        public abstract void CollectItem();
    }
}
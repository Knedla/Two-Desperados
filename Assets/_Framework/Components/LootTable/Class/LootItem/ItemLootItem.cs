using Game.Components.Notification;
using Game.System.Entity;

namespace Game.Components.LootTable
{
    public class ItemLootItem : LootItem
    {
        Item item;

        public ItemLootItem(Item item, int dropChance, int minQuantity = 1, int maxQuantity = 1) : base(dropChance, minQuantity, maxQuantity)
        {
            this.item = item;
        }

        public override void CollectItem() // zakucano, u sustini je dovoljno dobro za ovaj slucaj
        {
            Framework.PlayerData.Inventory.Add(item.Id, GetQuantity());
            Framework.EventManager.TriggerEvent(item.OnValueChangedTriggerName);
            Framework.NotificationManager.Notify(new InstantNotification(item.IconSprite, item.Name, item.Description));
        }
    }
}

using Game.Components.InventorySystem;
using Game.System.Entity;
using Game.System.Validation;
using System.Collections;

namespace Game.System.Action
{
    public abstract class ItemAction : IAction
    {
        protected IInventory inventory;
        public Item Item { get; }
        protected int quantity;

        public ItemAction(IInventory inventory, Item item, int quantity)
        {
            this.inventory = inventory;
            Item = item;
            this.quantity = quantity;
        }

        public virtual bool IsValid()
        {
            return new Validator<int>().AddRule(new HasEnoughQuantityValidationRule(inventory, Item)).Validate(quantity).IsValid;
        }

        public virtual IEnumerator Execute()
        {
            inventory.RemoveQuantity(Item.Id, quantity);
            Framework.EventManager.TriggerEvent(Item.OnValueChangedTriggerName);
            yield break;
        }
    }
}

using Game.System.Entity;
using Game.System.Validation;

namespace Game.Components.InventorySystem
{
    public class HasEnoughQuantityValidationRule : IValidationRule<int>
    {
        public ValidationError Error => new ValidationError(ErrorCode.Quantity_00001);

        Item item;
        IInventory inventory;

        public HasEnoughQuantityValidationRule(IInventory inventory, Item item)
        {
            this.inventory = inventory;
            this.item = item;
        }

        public bool Validate(int requiredAmount)
        {
            return inventory.GetQuantity(item.Id) >= requiredAmount;
        }
    }
}

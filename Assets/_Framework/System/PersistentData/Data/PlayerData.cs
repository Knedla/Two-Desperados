using Game.Components.Experience;
using Game.Components.InventorySystem;
using System;

namespace Game.System.PersistentData
{
    [Serializable]
    public class PlayerData : PersistentData
    {
        public override void OnDataChange()
        {
            if (Config.AutoSavetPersistData)
                LocalSave();
        }

        public IExperience Experience { get; private set; }
        public IInventory Inventory { get; private set; }

        public override void SetDefaultValues()
        {
            Experience = new Experience();
            Experience.SetDefaultValues();

            Inventory = new Inventory();
            Inventory.SetDefaultValues();
        }
    }
}

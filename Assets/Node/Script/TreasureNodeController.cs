using Game.Components.LootTable;

public class TreasureNodeController : NodeController, ILootable
{
    public override void OnHackedByPlayer()
    {
        Framework.EventManager.TriggerEvent(Game.System.Event.CustomListener.TreasureNodeHacked);
        base.OnHackedByPlayer();
    }

    public ILootItem GetLootItem()
    {
        return Framework.LootTableManager.GetLootItem((int)LootTableId.TreasureLootTable);
    }
}

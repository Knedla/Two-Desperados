public class SpamNodeController : NodeController
{
    public override int TriggeringPercent => 100;

    public override void OnHackedByPlayer() // bitan je redosled!!!
    {
        NetworkController.AddNetworkDifficulty(this, Player.Instance);
        Framework.EventManager.TriggerEvent(Game.System.Event.CustomListener.SpamNodeHacked);
        
        base.OnHackedByPlayer();
    }
}

using System.Collections;

public class StartNodeController : NodeController
{
    public override void OnHackedByPlayer()
    {
        hackedBy.Add(Player.Instance);
    }

    public override IEnumerator TriggerState()
    {
        Framework.EventManager.TriggerEvent(Game.System.Event.SystemListener.LevelLose);
        return base.TriggerState();
    }
}

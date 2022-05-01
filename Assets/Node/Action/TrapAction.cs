using Game.Components.Timer;
using Game.System.Entity;
using Game.System.State;
using System.Collections;

namespace Game.System.Action
{
    public class TrapAction : ItemAction
    {
        IUser user;
        NodeController nodeController;

        public TrapAction(IUser user, NodeController nodeController) : base(Framework.PlayerData.Inventory, new Trap(), 1)
        {
            this.user = user;
            this.nodeController = nodeController;
        }

        public override bool IsValid()
        {
            if (nodeController.State != NodeState.Unlocked)
                return false;

            return base.IsValid();
        }

        public override IEnumerator Execute()
        {
            yield return base.Execute();
            yield return new InProgressState(user, nodeController).SetState();
            yield return new WaitForSecondsList(nodeController.GetTimeToHack(user), nodeController.GetTimerViews(user));
            yield return new TrappedState(user, nodeController).SetState();
        }
    }
}

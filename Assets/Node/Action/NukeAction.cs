using Game.System.Entity;
using Game.System.State;
using System.Collections;

namespace Game.System.Action
{
    public class NukeAction : ItemAction
    {
        IUser user;
        NodeController nodeController;

        public NukeAction(IUser user, NodeController nodeController) : base(Framework.PlayerData.Inventory, new Nuke(), 1)
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
            yield return new HackedState(user, nodeController).SetState();
            nodeController.SetTimerViewsEndState(user);
        }
    }
}

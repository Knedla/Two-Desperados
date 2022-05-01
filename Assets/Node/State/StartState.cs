using System.Collections;

namespace Game.System.State
{
    public class StartState : BaseNodeState
    {
        public override NodeState State => NodeState.Hacked;

        public StartState(IUser user, NodeController nodeController) : base(user, nodeController) { }

        public override IEnumerator Enter()
        {
            nodeController.ResetGrayscale();
            nodeController.UnlockNeighbors(User);
            nodeController.Unlock();
            return base.Enter();
        }
    }
}

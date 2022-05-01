using System.Collections;

namespace Game.System.State
{
    public class HackedState : BaseNodeState
    {
        public override NodeState State => NodeState.Hacked;

        public HackedState(IUser user, NodeController nodeController) : base(user, nodeController) { }

        public override IEnumerator Enter()
        {
            nodeController.ResetGrayscale();
            nodeController.UnlockNeighbors(User);
            return base.Enter();
        }
    }
}

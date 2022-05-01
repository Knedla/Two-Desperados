namespace Game.System.State
{
    public class InProgressState : BaseNodeState
    {
        public override NodeState State => NodeState.InProgress;

        public InProgressState(IUser user, NodeController nodeController) : base(user, nodeController) { }
    }
}

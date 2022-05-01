using System.Collections;

namespace Game.System.State
{
    public class UnlockedState : BaseNodeState
    {
        public override NodeState State => NodeState.Unlocked;

        public UnlockedState(IUser user, NodeController nodeController) : base(user, nodeController) { }

        public override IEnumerator Enter()
        {
            nodeController.Unlock();
            
            Framework.EventManager.StartListening(Event.CustomListener.SpamNodeHacked, nodeController.RandomizeDifficulty);
            return base.Enter();
        }

        public override IEnumerator Exit()
        {
            Framework.EventManager.StopListening(Event.CustomListener.SpamNodeHacked, nodeController.RandomizeDifficulty);
            return base.Exit();
        }

        public override void OnDestroy()
        {
            Framework.EventManager.StopListening(Event.CustomListener.SpamNodeHacked, nodeController.RandomizeDifficulty);
            base.OnDestroy();
        }
    }
}

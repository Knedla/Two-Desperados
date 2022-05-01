using System.Collections;

namespace Game.System.State
{
    public abstract class BaseNodeState : IState
    {
        public abstract NodeState State { get; }

        public IUser User { get; private set; }

        protected NodeController nodeController;

        public BaseNodeState(IUser user, NodeController nodeController)
        {
            User = user;
            this.nodeController = nodeController;
        }

        public virtual IEnumerator SetState()
        {
            yield return nodeController.ChangeState(this);
        }

        public virtual IEnumerator Enter()
        {
            yield break;
        }

        public virtual IEnumerator Exit()
        {
            yield break;
        }

        public virtual IEnumerator Trigger()
        {
            yield break;
        }

        public virtual void OnDestroy() { }
    }
}

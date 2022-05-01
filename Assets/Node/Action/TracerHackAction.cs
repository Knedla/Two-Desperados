using Game.Components.Timer;
using System.Collections;

namespace Game.System.Action
{
    public class TracerHackAction : IAction
    {
        public enum ActionState
        {
            Initialized,
            InProgress,
            Executed
        }

        IUser user;
        NodeController nodeController;
        
        public ActionState State { get; private set; }

        public TracerHackAction(IUser user, NodeController nodeController)
        {
            this.user = user;
            this.nodeController = nodeController;
            State = ActionState.Initialized;
        }

        public bool IsValid()
        {
            return true;
        }

        public IEnumerator Execute()
        {
            State = ActionState.InProgress;
            yield return new WaitForSecondsList(nodeController.GetTimeToHack(user), nodeController.GetTimerViews(user));
            yield return nodeController.TriggerState();
            State = ActionState.Executed;
        }

        public IEnumerator WaitToFinish()
        {
            while(State != ActionState.Executed)
                yield return null;
        }

        public void SetTimerViewsEndState()
        {
            nodeController.SetTimerViewsEndState(user);
        }
    }
}

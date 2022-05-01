using Game.Components.Timer;
using Game.System.State;
using System.Collections;

namespace Game.System.Action
{
    public class HackAction : IAction
    {
        IUser user;
        protected NodeController nodeController;

        public HackAction(IUser user, NodeController nodeController)
        {
            this.user = user;
            this.nodeController = nodeController;
        }

        public bool IsValid()
        {
            return nodeController.State == NodeState.Unlocked;
        }

        public IEnumerator Execute() //Choosing the “hack” option starts the journey of the virus from one of the open neighbouring nodes to that target node - isto mi se vata, dal jedan il svi, a logicnije je svi, bar oni koji su trenutno otkljucani
        {
            yield return new InProgressState(user, nodeController).SetState();
            yield return new WaitForSecondsList(nodeController.GetTimeToHack(user), nodeController.GetTimerViews(user)); // mozda je lakse kroz animator, pa podesiti vreme trajanja animacije - mada bilo koja animacija na progres mi je ovako preciznija
            yield return new HackedState(user, nodeController).SetState();
        }
    }
}

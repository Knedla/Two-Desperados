using System.Collections;

namespace Game.System.State
{
    public abstract class StateMachine
    {
        IState currentState;

        public IEnumerable ChangeState(IState newState)
        {
            if (currentState != null)
                yield return Framework.StartCoroutine(currentState.Exit());

            currentState = newState;

            if (currentState != null)
                yield return Framework.StartCoroutine(currentState.Enter());
        }
    }
}
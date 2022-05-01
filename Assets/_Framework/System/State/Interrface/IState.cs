using System.Collections;

namespace Game.System.State
{
    public interface IState
    {
        IEnumerator Exit();
        IEnumerator Enter();
        IEnumerator Trigger();
    }
}

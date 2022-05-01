using System.Collections;

namespace Game.System.Action
{
    public interface IAction
    {
        bool IsValid();
        IEnumerator Execute();
    }
}

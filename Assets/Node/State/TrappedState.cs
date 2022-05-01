using Game.Components.Timer;
using Game.System.Entity;
using System.Collections;

namespace Game.System.State
{
    public class TrappedState : HackedState // u sustini je zbudeno - ako se javi potreba da jedan nod treba da ima vise modifiera, ova logika pada u vodu...
    {
        public override NodeState State => NodeState.Hacked;

        bool used;
        public TrappedState(IUser user, NodeController nodeController) : base(user, nodeController) { }

        // posto se nad nodom moze samo jednom izvrsiti akcija po useru, mogu da predpostavim da bilo ko ko sledeci dodje na nod sa trap-om ce okinuti trap
        // u suprotnom, da nod mora svaki put da se hakuje, onda bi morao da se uvede property Owner, na osnovu koga bi mogao da razlikujem ko ce da okine trap
        public override IEnumerator Enter()
        {
            nodeController.SetModifier(new Trap());
            return base.Enter();
        }

        public override IEnumerator Trigger()
        {
            if (used)
                yield break;

            used = true;
            yield return base.Trigger();
            nodeController.TimerView.Show();
            yield return new WaitForSeconds(Framework.PlayerPreferenceData.TrapDelayTime, nodeController.TimerView);
            nodeController.TimerView.Hide();
            nodeController.ResetModifier();
        }
    }
}

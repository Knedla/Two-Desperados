using Game.System.Entity.Database;
using System;

namespace Game.System.Entity
{
    [Serializable]
    public class Trap : Item
    {
        [NonSerialized]
        static Definition.Trap definition = (Definition.Trap)Database<Definition.Item>.Instance.GetDefinition<Definition.Trap>();
        public override Definition.Item Definition => definition;
    }
    //da li trap posle prvog okidanja prestane da postoji - logicno da da, al sta znam...
    //da li trap moze da se stavi na nodove koje je igrac vec hakovao
    //da li uopste ima potreba da se otvara action panel na nodu koji je hakovan od strane igraca
    //uzecu da je one action per node per user...
}

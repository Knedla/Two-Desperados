using Game.System.Entity.Database;
using System;

namespace Game.System.Entity
{
    [Serializable]
    public class Nuke : Item
    {
        [NonSerialized]
        static Definition.Nuke definition = (Definition.Nuke)Database<Definition.Item>.Instance.GetDefinition<Definition.Nuke>();
        public override Definition.Item Definition => definition;
    }
}

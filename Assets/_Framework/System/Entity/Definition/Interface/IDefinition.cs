using System;

namespace Game.System.Entity.Definition
{
    public interface IDefinition
    {
        int Id { get; }
        Type Type { get; }
    }
}

using UnityEngine;

namespace Game.Common
{
    public interface IPresentation
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
        Sprite IconSprite { get; }
        Sprite ButtonSprite { get; }
    }
}

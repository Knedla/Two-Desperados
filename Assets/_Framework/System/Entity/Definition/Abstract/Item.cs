using Game.Common;
using System;
using UnityEngine;

namespace Game.System.Entity.Definition
{
    public abstract partial class Item : BaseScriptableObject, IPresentation, IDefinition
    {
        string ResourceString { get { return Type.Name.ToLower(); } }
        public string Name => Config.NameResourcePrefix + ResourceString;
        public string Description => Config.DescriptionResourcePrefix + ResourceString;
        public string OnValueChangedTriggerName => Config.OnValueChangedTriggerPrefix + ResourceString;

        public Sprite iconSprite;
        public Sprite IconSprite => iconSprite;

        public Sprite buttonSprite;
        public Sprite ButtonSprite => buttonSprite;

        Type type;
        public Type Type
        {
            get
            {
                if (type == null)
                    type = GetType();

                return type;
            }
        }
    }
}

using Game.Common;
using System;
using UnityEngine;

namespace Game.System.Entity
{
    public abstract class Item : IPresentation // inicijalno pravljeno ako item ima instancu => generic axe strength 2 
    {
        public abstract Definition.Item Definition { get; }

        public int Id => Definition.Id; // drugacije bi morao da se dodeljuje Id u slucaju da postoji instance objekata sa razlicitim vrednostima propertija - za ove potrebe je ovo dovoljno dobro
        public string Name => Definition.Name;
        public string Description => Definition.Description;
        public Sprite IconSprite => Definition.IconSprite;
        public Sprite ButtonSprite => Definition.ButtonSprite;
        public string OnValueChangedTriggerName => Definition.OnValueChangedTriggerName;

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

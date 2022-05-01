using System;

namespace Game.Components.Quest
{
    public abstract class QuestData
    {
        public abstract Type QuestControllerType { get; }
        
        float value;
        public float Value { get { return value; } }

        public QuestData(float value)
        {
            this.value = value;
        }
    }
}

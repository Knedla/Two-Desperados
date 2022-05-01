using Game.System.Event;
using System;

namespace Game.Components.Quest
{
    public class CollectQuestData : QuestData
    {
        public override Type QuestControllerType { get { return typeof(CollectQuestController); } }
        public GameController GameController { get; private set; }
        public CustomListener CustomListener { get; set; }

        public CollectQuestData(GameController gameController, CustomListener customListener, float value) : base(value)
        {
            GameController = gameController;
            CustomListener = customListener;
        }
    }
}

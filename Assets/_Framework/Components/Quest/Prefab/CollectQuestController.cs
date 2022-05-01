using Game.System.Event;

namespace Game.Components.Quest
{
    public class CollectQuestController : QuestController
    {
        CustomListener customListener;
        CollectQuestData data;

        public override void SetData(QuestData questData)
        {
            Done = false;
            data = questData as CollectQuestData;
            RequiredValue = data.Value;
            customListener = data.CustomListener;

            Framework.EventManager.TriggerEvent(CustomListener.QuestValueChanged);
            Framework.EventManager.StartListening(customListener, GetItem);
        }

        void GetItem()
        {
            if (data.GameController.GameState == GameState.Lose || Done)
                return;

            CurrentValue += 1;
            Framework.EventManager.TriggerEvent(CustomListener.QuestValueChanged);

            if (CurrentValue >= RequiredValue)
            {
                Done = true;
                Framework.EventManager.TriggerEvent(CustomListener.QuestDone);
            }
        }

        public override void OnDestroy()
        {
            Framework.EventManager.StopListening(customListener, GetItem);
        }
    }
}

using System;

namespace Game.Components.Experience
{
    [Serializable]
    public class Experience : IExperience
    {
        public void OnDataChange()
        {
            Framework.PlayerData.OnDataChange();
        }

        public int Value { get; set; }

        public void Add(int value)
        {
            Value += value;
            OnDataChange();
            Framework.EventManager.TriggerEvent(System.Event.CustomListener.OnExperienceQuantityChange);
        }

        public void SetDefaultValues()
        {
            Value = 0;
        }

        public void LocalSave()
        {
            Framework.PlayerData.LocalSave();
        }

        public void UploadToCloud()
        {
            Framework.PlayerData.UploadToCloud();
        }
    }
}
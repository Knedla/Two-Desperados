using System;

namespace Game.System.PersistentData
{
    [Serializable]
    public abstract class PreferencePersistentData : IPersistentData
    {
        public abstract void OnDataChange();

        public Components.IO.Audio.IData AudioData { get; private set; }

        public virtual void SetDefaultValues()
        {
            AudioData = new Components.IO.Audio.Data();
            AudioData.SetDefaultValues();
        }

        public void LocalSave()
        {
            DataExtension.SaveDataToFile(this, Config.PlayerPreferencesDataFileName.GetPersistentDataFullPath());
        }

        public void UploadToCloud() { }
    }
}

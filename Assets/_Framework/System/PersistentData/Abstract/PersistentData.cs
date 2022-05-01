using System;

namespace Game.System.PersistentData
{
    [Serializable]
    public abstract class PersistentData : IPersistentData
    {
        public abstract void OnDataChange();
        public abstract void SetDefaultValues();

        public void LocalSave()
        {
            DataExtension.SaveDataToFile(this, Config.PersistDataFileName.GetPersistentDataFullPath());
        }

        public void UploadToCloud() { }
    }
}

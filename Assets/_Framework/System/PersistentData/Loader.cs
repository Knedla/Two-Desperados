namespace Game.System.PersistentData
{
    public static class Loader
    {
        public static PlayerPreferenceData LoadPlayerPreferenceData()
        {
            return Load<PlayerPreferenceData>(Config.PlayerPreferencesDataFileName);
        }

        public static PlayerData LoadPlayerData()
        {
            return Load<PlayerData>(Config.PersistDataFileName);
        }

        static T Load<T>(string persistDataFileName) where T : class, IPersistentData, new()
        {
            T persistentData = DataExtension.LoadDataFromFile<T>(persistDataFileName.GetPersistentDataFullPath());
            if (persistentData == null)
                persistentData = Initialize<T>();
            return persistentData;
        }

        static T Initialize<T>() where T : class, IPersistentData, new()
        {
            T persistentData = new T();
            persistentData.SetDefaultValues();
            persistentData.LocalSave();
            return persistentData;
        }
    }
}

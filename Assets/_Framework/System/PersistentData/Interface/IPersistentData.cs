namespace Game.System.PersistentData
{
    public interface IPersistentData
    {
        void OnDataChange(); // nije najsrecnije... ovako mi je centralizovan autosave, onao bi svaki IPersistentData imao svoj Config.AutoSave i moglo bi da se desi nekonzistentnost - u istom fajlu, jedno je podeseno da cuva automatski, a drugo da ne cuva dok ne okine validaciju - ako izmenis ovo prvo, zapamtice cak i ako su nevalidne vrednosti u drugom
        void SetDefaultValues();
        void LocalSave();
        void UploadToCloud();
    }
}
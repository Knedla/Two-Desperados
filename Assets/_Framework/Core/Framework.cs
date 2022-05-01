using System.Collections;
using UnityEngine;

public static class Framework
{
    static Framework()
    {
        EventManager = new Game.System.Event.Manager();
        PoolManager = new Game.System.Pooling.Manager();
        NotificationManager = new Game.Components.Notification.Manager();
        EscapeKeyManager = new Game.Components.IO.PlayerInput.EscapeKey.Manager();

        PlayerData = Game.System.PersistentData.Loader.LoadPlayerData();
        PlayerPreferenceData = Game.System.PersistentData.Loader.LoadPlayerPreferenceData();

        AudioManager = new Game.Components.IO.Audio.Manager();
        ExperienceManager = new Game.Components.Experience.Manager();
        LootTableManager = new Game.Components.LootTable.Manager();
    }

    #region Helpers
    public static Game.System.Event.IManager EventManager { get; private set; }
    public static Game.System.Pooling.IManager PoolManager { get; private set; }
    public static Game.Components.Notification.IManager NotificationManager { get; private set; }
    public static Game.Components.IO.PlayerInput.EscapeKey.Manager EscapeKeyManager { get; private set; }
    #endregion Helpers

    #region Persistent Data
    public static Game.System.PersistentData.PlayerData PlayerData { get; private set; }
    public static Game.System.PersistentData.PlayerPreferenceData PlayerPreferenceData { get; private set; }
    #endregion Persistent Data

    #region Audio
    public static Game.Components.IO.Audio.IManager AudioManager { get; private set; }
    public static Game.Components.IO.Audio.IData AudioData { get { return PlayerPreferenceData.AudioData; } }
    #endregion Audio

    #region Ingame Systems
    //public static Game.Components.ExperienceSystem.IExperience Experience { get { return PlayerData.Experience; } }
    //public static Game.Components.InventorySystem.IInventory Inventory { get { return PlayerData.Inventory; } }
    public static Game.Components.Experience.Manager ExperienceManager { get; private set; }
    public static Game.Components.LootTable.IManager LootTableManager { get; private set; }
    #endregion

    #region MonoBehaviour
    public static GameObject GameObject { get { return Game.Core.Init.Instance.gameObject; } }

    public static Coroutine StartCoroutine(IEnumerator routine)
    {
        return Game.Core.Init.Instance.StartCoroutine(routine);
    }

    public static void StopCoroutine(IEnumerator routine)
    {
        if (Game.Core.Init.Instance)
            Game.Core.Init.Instance.StopCoroutine(routine);
    }

    public static void StopAllCoroutines()
    {
        if (Game.Core.Init.Instance)
            Game.Core.Init.Instance.StopAllCoroutines();
    }
    #endregion MonoBehaviour
}

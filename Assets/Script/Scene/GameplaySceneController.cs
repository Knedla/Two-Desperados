using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplaySceneController : MonoBehaviour
{
    public const string SceneName = "Gameplay";
    
    public static void LoadScene()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }

    public static void ResetScene()
    {
        Framework.EventManager.TriggerEvent(Game.System.Event.SystemListener.PreserveDataBetweenReload);
        SceneManager.LoadSceneAsync(SceneName);
    }
}

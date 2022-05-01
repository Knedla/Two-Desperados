using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour // moglo bi se ubaciti loading kad se dolazi na MainMenu i Gameplay scenu; trenutno overlay radi poso, osim kad se pokrene igra, tad malo zastuca, bar na mojoj masini
{
    public const string SceneName = "MainMenu";

    public static void LoadScene()
    {
        SceneManager.LoadSceneAsync(SceneName);
    }
}

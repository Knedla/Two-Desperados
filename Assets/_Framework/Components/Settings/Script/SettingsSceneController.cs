using Game.Components.UI.Window;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Components.Settings
{
    public class SettingsSceneController : MonoBehaviour
    {
        public const string SceneName = "Settings";

        static bool isActive;
        public static void LoadScene()
        {
            if (!isActive)
                SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        }

        public Controller UIWindow;

        private void Awake()
        {
            isActive = true;
        }

        void Start()
        {
            UIWindow.Open();
            UIWindow.CloseAnimationEndedAction = () => SceneManager.UnloadSceneAsync(SceneName);
        }

        private void OnDestroy()
        {
            isActive = false;
        }
    }
}
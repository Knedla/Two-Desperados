using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class Init : MonoBehaviour
    {
        public static Init Instance { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.LoadScene(Config.AfterInitSceneName);
        }
    }
}
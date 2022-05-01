using Game.Components.UI;
using Game.Components.UI.Window;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuSceneController : MonoBehaviour
{
    const string winText = "Win";
    const string loseText = "Lose";
    const string pauseText = "Pause";

    public const string SceneName = "GameMenu";

    public static bool isActive;
    static GameState gameState;
    public static void LoadScene(GameState gameState)
    {
        if (isActive)
            return;

        isActive = true;
        GameMenuSceneController.gameState = gameState;
        SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
    }

    [SerializeField] private Text TitleText;
    [SerializeField] private Controller UIWindow;
    [SerializeField] private CustomButton ContinueButton;
    [SerializeField] private Button LevelRestartButton;

    private void Awake()
    {
        if (gameState == GameState.Win)
        {
            TitleText.text = winText;
            ContinueButton.gameObject.SetActive(false);
        }
        else if (gameState == GameState.Lose)
        {
            TitleText.text = loseText;
            ContinueButton.gameObject.SetActive(false);
        }
        else
        {
            TitleText.text = pauseText;
            ContinueButton.onClick.AddListener(UIWindow.Close);
        }

        LevelRestartButton.onClick.AddListener(GameplaySceneController.ResetScene);
    }

    void Start()
    {
        UIWindow.Open();
        UIWindow.CloseAnimationEndedAction = Close;
        Framework.EscapeKeyManager.Block();
    }

    public void Close()
    {
        SceneManager.UnloadSceneAsync(SceneName);
        Framework.EventManager.TriggerEvent(Game.System.Event.SystemListener.GameMenuClosed);
    }

    private void OnDestroy()
    {
        Framework.EscapeKeyManager.Unblock();
        isActive = false;
    }
}

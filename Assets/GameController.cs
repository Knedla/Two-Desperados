using Game.Components.Quest;
using System;
using UnityEngine;

public class GameController : Game.Components.IO.PlayerInput.EscapeKey.Item
{
    static GameController instance;

    public GameState GameState { get; private set; }

    QuestController questController;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        GameState = GameState.Play;

        Framework.EventManager.StartListening(Game.System.Event.CustomListener.QuestDone, LevelWin);
        Framework.EventManager.StartListening(Game.System.Event.SystemListener.LevelLose, LevelLose);
    }

    private void Start()
    {
        SetQuest();
    }

    void SetQuest()
    {
        QuestData questData = GetQuestData();
        questController = (QuestController)Activator.CreateInstance(questData.QuestControllerType); // dinamicko instanciranje questa
        questController.SetData(questData);
    }

    QuestData GetQuestData()
    {
        return new CollectQuestData(this, Game.System.Event.CustomListener.TreasureNodeHacked, NetworkController.NetworkData.TreasureNodeCount);
    }

    void LevelWin()
    {
        Framework.StopAllCoroutines();
        GameState = GameState.Win;
        GameMenuSceneController.LoadScene(GameState);
    }

    void LevelLose()
    {
        Framework.StopAllCoroutines();
        GameState = GameState.Lose;
        GameMenuSceneController.LoadScene(GameState);
    }

    void LevelPause()
    {
        if (GameState == GameState.Play)
        {
            GameState = GameState.Pause;
            Time.timeScale = 0;
        }
        else if (GameState == GameState.Pause)
        {
            GameState = GameState.Play;
            Time.timeScale = 1;
        }
    }

    public override void EscapeKeyPressed()
    {
        LevelPause();
        Framework.EventManager.StartListening(Game.System.Event.SystemListener.GameMenuClosed, GameMenuClosed);
        GameMenuSceneController.LoadScene(GameState);
    }

    void GameMenuClosed()
    {
        LevelPause();
        Framework.EventManager.StopListening(Game.System.Event.SystemListener.GameMenuClosed, GameMenuClosed);
    }

    private void OnDestroy()
    {
        if (instance != this)
            return;

        Framework.StopAllCoroutines();

        instance = null;
        Time.timeScale = 1;

        questController.OnDestroy();
        Framework.EventManager.StopListening(Game.System.Event.CustomListener.QuestDone, LevelWin);
        Framework.EventManager.StopListening(Game.System.Event.SystemListener.LevelLose, LevelLose);
        Framework.EventManager.StopListening(Game.System.Event.SystemListener.GameMenuClosed, GameMenuClosed);
    }
}

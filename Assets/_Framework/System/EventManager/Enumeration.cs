namespace Game.System.Event
{
    public enum SystemListener
    {
        OnSceneOwerlayAnimationEnded,
        OnButtonClick,
        LevelWin,
        LevelLose,
        GameMenuClosed,
        PreserveDataBetweenReload,
    }

    public enum CustomListener
    {
        QuestValueChanged,
        QuestDone,

        TreasureNodeHacked,
        OnExperienceQuantityChange,
        NodeHacked,
        SpamNodeHacked,
        ActivateTracers,
    }
}

using System;

namespace Game.System.PersistentData
{
    [Serializable]
    public class PlayerPreferenceData : PreferencePersistentData
    {
        public override void OnDataChange()
        {
            if (Config.AutoSavetPlayerPreference)
                LocalSave();
        }

        public int NodeCount { get; set; }
        public int NodeCountIncludeStartNode { get { return NodeCount + 1; } }
        public int TreasureNodeCount { get; set; }
        public int FirewallNodeCount { get; set; }
        public int SpamNodeCount { get; set; }
        public int SpamNodeDecrease { get; set; }
        public int TrapDelayTime { get; set; }

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            NodeCount = 19;
            TreasureNodeCount = 7;
            FirewallNodeCount = 3;
            SpamNodeCount = 5;
            SpamNodeDecrease = 3;
            TrapDelayTime = 4;
        }
    }
}

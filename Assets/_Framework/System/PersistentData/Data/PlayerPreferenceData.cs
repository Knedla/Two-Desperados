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

            NodeCount = 3;
            TreasureNodeCount = 1;
            FirewallNodeCount = 1;
            SpamNodeCount = 1;
            SpamNodeDecrease = 0;
            TrapDelayTime = 0;
        }
    }
}

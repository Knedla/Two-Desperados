public class TreasureNodeCountInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinTreasureNodeCount;
    protected override int? MaxValue => int.MaxValue;
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.TreasureNodeCount;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.TreasureNodeCount = value;
    }
}

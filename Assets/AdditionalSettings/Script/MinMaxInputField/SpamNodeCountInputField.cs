public class SpamNodeCountInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinSpamNodeCount;
    protected override int? MaxValue => int.MaxValue;
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.SpamNodeCount;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.SpamNodeCount = value;
    }
}

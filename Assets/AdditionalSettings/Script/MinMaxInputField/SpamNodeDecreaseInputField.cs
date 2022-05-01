public class SpamNodeDecreaseInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinSpamNodeDecrease;
    protected override int? MaxValue => 100;
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.SpamNodeDecrease;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.SpamNodeDecrease = value;
    }
}

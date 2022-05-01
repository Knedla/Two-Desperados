public class TrapDelayTimeInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinTrapDelayTime;
    protected override int? MaxValue => Config.MaxTrapDelayTime;
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.TrapDelayTime;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.TrapDelayTime = value;
    }
}

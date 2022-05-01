public class NodeCountInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinNodeCount;
    protected override int? MaxValue => Config.MaxNodeCount;
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.NodeCount;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.NodeCount = value;
    }
}

public class FirewallNodeCountInputField : MinMaxInputField
{
    protected override int? MinValue => Config.MinFirewallNodeCount;
    protected override int? MaxValue => int.MaxValue; // iako je bilo definisano null, kad se proisledi validatoru, prosledi "0" - nisam imao vremena da trazim zasto tako radi...
    protected override int GetValue()
    {
        return Framework.PlayerPreferenceData.FirewallNodeCount;
    }
    protected override void SetValue(int value)
    {
        Framework.PlayerPreferenceData.FirewallNodeCount = value;
    }
}

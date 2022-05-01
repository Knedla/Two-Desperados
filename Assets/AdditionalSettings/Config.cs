public partial class Config
{
    public const int MinNodeCount = MinTreasureNodeCount + MinFirewallNodeCount + MinSpamNodeCount;
    public const int MaxNodeCount = 100;

    public const int MinTreasureNodeCount = 1;
    public const int MinFirewallNodeCount = 1;
    public const int MinSpamNodeCount = 1;

    public const int MinSpamNodeDecrease = 0;

    public const int MinTrapDelayTime = 0;
    public const int MaxTrapDelayTime = 100;
}

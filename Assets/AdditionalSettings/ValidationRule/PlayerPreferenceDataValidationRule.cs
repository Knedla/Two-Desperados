using Game.System.PersistentData;
using Game.System.Validation;

public class PlayerPreferenceDataValidationRule : IValidationRule<PlayerPreferenceData>
{
    public ValidationError Error => new ValidationError(ErrorCode.Settings_00001);

    public bool Validate(PlayerPreferenceData model)
    {
        return model.NodeCount >= model.TreasureNodeCount + model.FirewallNodeCount + model.SpamNodeCount;
    }
}

using Game.System.Validation;

public class IsInRangeValidationRule : IValidationRule<int>
{
    public ValidationError Error => new ValidationError(ErrorCode.Settings_00001);

    int? min;
    int? max;

    public IsInRangeValidationRule(int? min, int? max)
    {
        this.min = min;
        this.max = max;
    }

    public bool Validate(int model)
    {
        return (min != null && min.Value <= model) && (max != null && model <= max.Value );
    }
}

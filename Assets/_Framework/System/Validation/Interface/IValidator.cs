namespace Game.System.Validation
{
    public interface IValidator<T>
    {
        IValidator<T> AddRule(IValidationRule<T> rule);
        ValidationResult Validate(T model);
    }
}

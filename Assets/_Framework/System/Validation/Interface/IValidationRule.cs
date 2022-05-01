namespace Game.System.Validation
{
    public interface IValidationRule<T>
    {
        ValidationError Error { get; }
        bool Validate(T model);
    }
}

namespace Game.System.Validation
{
    public class ValidationError
    {
        public ErrorCode? Code { get; private set; }

        public ValidationError(ErrorCode? code)
        {
            Code = code;
        }
    }
}

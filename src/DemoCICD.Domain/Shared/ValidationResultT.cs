namespace DemoCICD.Domain.Shared;
public class ValidationResult<T> : Result<T>, IValidationResult
{
    public Error[] Errors { get; }

    public ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public static ValidationResult<T> WithErrors(Error[] errors) => new(errors);
}

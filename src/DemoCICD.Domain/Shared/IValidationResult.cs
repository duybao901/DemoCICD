namespace DemoCICD.Domain.Shared;
public interface IValidationResult
{
    public static readonly Error ValidationError = new Error("ValidationError", " validation problem occurred.");

    Error[] Errors { get; }
}

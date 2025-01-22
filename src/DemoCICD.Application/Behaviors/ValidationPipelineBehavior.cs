using DemoCICD.Contract.Abstractions.Shared;
using FluentValidation;
using MediatR;

namespace DemoCICD.Application.Behaviors;
public sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        // Nếu TResult là Result, trả về một đối tượng ValidationResult dưới dạng Result
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult) !;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors)) !
            .Invoke(null, new object?[] { errors }) !;

        return (TResult)validationResult;
        /*
            typeof(ValidationResult<>): Lấy định nghĩa kiểu tổng quát ValidationResult<> (ví dụ: ValidationResult<T>).

            GetGenericTypeDefinition(): Lấy loại tổng quát cơ bản (ValidationResult<>) mà không chứa tham số kiểu cụ thể.

            MakeGenericType(typeof(TResult).GenericTypeArguments[0]):

            typeof(TResult).GenericTypeArguments[0]: Lấy tham số kiểu đầu tiên của TResult (ví dụ: nếu TResult là Result<int>, tham số này là int).
            MakeGenericType(...): Tạo một kiểu tổng quát ValidationResult<int> (nếu tham số là int).
            GetMethod(nameof(ValidationResult.WithErrors)):

            Lấy thông tin về phương thức WithErrors trong lớp ValidationResult<T>.
            Invoke(...):

            Gọi phương thức WithErrors với danh sách lỗi (errors).
            null: Vì đây là một phương thức tĩnh nên không cần tham chiếu đến đối tượng cụ thể.
            new object?[] { errors }: Đối số truyền vào phương thức WithErrors.
            (TResult)validationResult:

            Chuyển kiểu đối tượng trả về thành TResult.
         */
    }
}

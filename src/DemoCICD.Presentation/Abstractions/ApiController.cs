using DemoCICD.Contract.Abstractions.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    public static IActionResult HandleFailureReturn(Result result)
        => result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                new BadRequestObjectResult(CreateProblemDetails(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    error: result.Error,
                    errors: validationResult.Errors)),
            _ => new BadRequestObjectResult(CreateProblemDetails(
                    title: "Bad Request",
                    status: StatusCodes.Status400BadRequest,
                    error: result.Error)),
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}

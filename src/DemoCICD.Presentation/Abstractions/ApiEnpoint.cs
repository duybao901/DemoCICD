using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCICD.Contract.Abstractions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoCICD.Presentation.Abstractions;
public abstract class ApiEnpoint
{
    public static IResult HandleFailureReturn(Result result)
    => result switch
    {
        { IsSuccess: true } => throw new InvalidOperationException(),
        IValidationResult validationResult =>
            Results.BadRequest(CreateProblemDetails(
                title: "Bad Request",
                status: StatusCodes.Status400BadRequest,
                error: result.Error,
                errors: validationResult.Errors)),
        _ => Results.BadRequest(CreateProblemDetails(
                title: "Bad Request",
                status: StatusCodes.Status400BadRequest,
                error: result.Error)),
    };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new ()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}

using DemoCICD.Domain.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions;
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

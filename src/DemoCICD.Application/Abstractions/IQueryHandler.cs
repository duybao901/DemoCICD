using DemoCICD.Domain.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

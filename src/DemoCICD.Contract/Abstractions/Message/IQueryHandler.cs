using MediatR;

namespace DemoCICD.Contract.Abstractions.Shared;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}

using MediatR;

namespace DemoCICD.Contract.Abstractions.Shared;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>    
{
}

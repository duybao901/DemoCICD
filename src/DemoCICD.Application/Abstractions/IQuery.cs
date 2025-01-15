using DemoCICD.Domain.Shared;
using MediatR;

namespace DemoCICD.Application.Abstractions;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>    
{
}

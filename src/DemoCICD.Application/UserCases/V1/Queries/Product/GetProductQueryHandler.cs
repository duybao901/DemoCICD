using DemoCICD.Application.Abstractions;
using DemoCICD.Domain.Shared;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResponse>
{
    public Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

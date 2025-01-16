using DemoCICD.Application.Abstractions;
using DemoCICD.Domain.Shared;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, GetProductResponse>
{
    public Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

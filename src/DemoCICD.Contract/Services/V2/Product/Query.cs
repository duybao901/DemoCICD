using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using static DemoCICD.Contract.Services.V2.Product.Response;

namespace DemoCICD.Contract.Services.V2.Product;
public static class Query
{
    public record GetProductQuery() : IQuery<List<ProductResponse>>;

    public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
}

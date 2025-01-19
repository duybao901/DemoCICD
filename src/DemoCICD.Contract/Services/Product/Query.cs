using DemoCICD.Contract.Abstractions.Shared;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product;
public static class Query
{
    public record GetProductQuery() : IQuery<List<ProductResponse>>;

    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
}

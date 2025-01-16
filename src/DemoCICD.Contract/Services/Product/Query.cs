using DemoCICD.Contract.Abstractions.Shared;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product;
public static class Query
{
    public record getProductQuery() : IQuery<List<ProductResponse>>;

    public record getProductById(Guid Id) : IQuery<ProductResponse>;
}

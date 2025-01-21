using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using static DemoCICD.Contract.Services.Product.Response;

namespace DemoCICD.Contract.Services.Product;
public static class Query
{
    public record GetProductQuery(string? searchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<List<ProductResponse>>;

    public record GetProductById(Guid Id) : IQuery<ProductResponse>;
}

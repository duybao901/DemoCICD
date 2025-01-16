using DemoCICD.Application.Abstractions;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public class GetProductQuery : IQuery<GetProductResponse>
{
    public string Name { get; set; }

    public double Price { get; set; }
}

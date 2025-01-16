using DemoCICD.Contract.Abstractions.Shared;

namespace DemoCICD.Contract.Services.Product;
public static class Command
{
    public record CreateProduct(string Name, decimal Price, string Description) : ICommand;

    public record UpdateProduct(Guid Id, string Name, decimal Price, string Description) : ICommand;

    public record DeleteProduct(Guid Id) : ICommand;
}

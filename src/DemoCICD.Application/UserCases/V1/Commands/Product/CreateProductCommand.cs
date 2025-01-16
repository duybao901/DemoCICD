using DemoCICD.Contract.Abstractions.Shared;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public class CreateProductCommand : ICommand
{
    public string Name { get; set; }

    public double Price { get; set; }
}

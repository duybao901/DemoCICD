using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProduct>
{
    public Task<Result> Handle(Command.CreateProduct request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

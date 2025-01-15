using DemoCICD.Application.Abstractions;
using DemoCICD.Domain.Shared;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
{
    public Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

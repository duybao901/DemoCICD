using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V2.Commands;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id)
            ?? throw new ProductException.ProductNotFoundException(request.Id);

        await _unitOfWork.ProductRepository.DeleteAsync(request.Id);
        return Result.Success();
    }
}

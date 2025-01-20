using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProduct>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, IUnitOfWork unitOfWork)
    {
        _productRepositoryBase = productRepositoryBase;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.DeleteProduct request, CancellationToken cancellationToken)
    {
        var product = await _productRepositoryBase.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);

        _productRepositoryBase.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

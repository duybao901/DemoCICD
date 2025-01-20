using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class UpdateProductCommandHandler : ICommandHandler<Command.UpdateProduct>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, IUnitOfWork unitOfWork)
    {
        _productRepositoryBase = productRepositoryBase;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.UpdateProduct request, CancellationToken cancellationToken)
    {
        var product = await _productRepositoryBase.FindByIdAsync(request.Id) ?? throw new Exception("Product not found");

        product.Update(request.Name, request.Price, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

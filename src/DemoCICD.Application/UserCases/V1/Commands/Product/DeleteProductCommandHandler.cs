using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, 
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _productRepositoryBase = productRepositoryBase;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepositoryBase.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id);

        _productRepositoryBase.Remove(product);
        // await _unitOfWork.SaveChangesAsync(cancellationToken); Commented for TransactionScope

        await _publisher.Publish(new DomainEvent.ProductDeleted(product.Id), cancellationToken);

        return Result.Success();
    }
}

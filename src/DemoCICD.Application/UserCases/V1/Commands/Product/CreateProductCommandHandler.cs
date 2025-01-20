using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProduct>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _productRepositoryBase = repositoryBase;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(Command.CreateProduct request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(new Guid(), request.Name, request.Price, request.Description);
        _productRepositoryBase.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

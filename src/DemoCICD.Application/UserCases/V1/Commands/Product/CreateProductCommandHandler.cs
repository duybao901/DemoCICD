using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Domain.Exceptions;
using DemoCICD.Persistence;
using MediatR;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, 
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ApplicationDbContext context,
        IPublisher publisher)
    {
        _productRepositoryBase = repositoryBase;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _context = context;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(new Guid(), request.Name, request.Price, request.Description);
        _productRepositoryBase.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        //// Try to get product ID
        var productCreated = await _productRepositoryBase.FindByIdAsync(product.Id)
            ?? throw new ProductException.ProductNotFoundException(product.Id);

        //var productSecond = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), productCreated.Name + " Second",
        //    productCreated.Price,
        //    productCreated.Id.ToString());

        //_productRepositoryBase.Add(productSecond);
        ////await _unitOfWork.SaveChangesAsync(cancellationToken);
        //await _context.SaveChangesAsync();

        await _publisher.Publish(new DomainEvent.ProductCreated(product.Id), cancellationToken);

        return Result.Success();
    }
}

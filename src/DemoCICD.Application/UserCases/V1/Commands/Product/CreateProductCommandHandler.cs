using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Commands.Product;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProduct>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> repositoryBase, IMapper mapper, IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _productRepositoryBase = repositoryBase;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<Result> Handle(Command.CreateProduct request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(new Guid(), request.Name, request.Price, request.Description);
        _productRepositoryBase.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        //// Try to get product ID
        // var productCreated = await _productRepositoryBase.FindByIdAsync(product.Id);

        //var productSecond = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), productCreated.Name + " Second",
        //    productCreated.Price,
        //    productCreated.Id.ToString());

        //_productRepositoryBase.Add(productSecond);
        ////await _unitOfWork.SaveChangesAsync(cancellationToken);
        //await _context.SaveChangesAsync();
        return Result.Success();
    }
}

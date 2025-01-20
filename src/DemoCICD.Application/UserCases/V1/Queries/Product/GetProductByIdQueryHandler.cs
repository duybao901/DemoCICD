using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions.Repositories;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductByIdQueryHandler : IQueryHandler<Query.GetProductById, Response.ProductResponse>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, IMapper mapper)
    {
        _productRepositoryBase = productRepositoryBase;
        _mapper = mapper;
    }

    public async Task<Result<Response.ProductResponse>> Handle(Query.GetProductById request, CancellationToken cancellationToken)
    {
        var item = await _productRepositoryBase.FindByIdAsync(request.Id);
        var result = _mapper.Map<Response.ProductResponse>(item);
        return Result.Success(result);
    }
}

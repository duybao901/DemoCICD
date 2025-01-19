using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, IMapper mapper)
    {
        _productRepositoryBase = productRepositoryBase;
        _mapper = mapper;
    }

    public async Task<Result<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepositoryBase.FindAll().ToListAsync();
        var result = _mapper.Map<List<Response.ProductResponse>>(products);
        return result;
    }
}

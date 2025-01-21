using System.Linq.Expressions;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
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
        var productQuery = string.IsNullOrEmpty(request.searchTerm) ?
            _productRepositoryBase.FindAll() :
            _productRepositoryBase.FindAll(x => x.Name.Contains(request.searchTerm) || x.Description.Contains(request.searchTerm));

        Expression<Func<Domain.Entities.Product, object>> keySelector = request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "description" => product => product.Description,
            "price" => product => product.Price,
            _ => product => product.Id // REAL: product => product.CreatedDate by default
        };

        productQuery = request.SortOrder == SortOrder.Ascending ?
            productQuery.OrderBy(keySelector) :
            productQuery.OrderByDescending(keySelector);

        var products = await productQuery.ToListAsync();
        var result = _mapper.Map<List<Response.ProductResponse>>(products);
        return result;
    }
}

using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.Product;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepositoryBase;
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _context;

    public GetProductQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepositoryBase, IMapper mapper, ApplicationDbContext context)
    {
        _productRepositoryBase = productRepositoryBase;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        if (request.SortColumnAndOrder.Any())
        {
            var productQuery = new StringBuilder();
            if (string.IsNullOrWhiteSpace(request.searchTerm))
            {
                productQuery.Append($"SELECT * FROM {nameof(Domain.Entities.Product)} ORDER BY ");
            }
            else
            {
                productQuery.Append($"SELECT * FROM {nameof(Domain.Entities.Product)} WHERE {nameof(Domain.Entities.Product.Name)} LIKE '%{request.searchTerm}%' OR {nameof(Domain.Entities.Product.Description)} LIKE '%{request.searchTerm}%' ORDER BY ");
            }

            foreach (var item in request.SortColumnAndOrder)
            {
                productQuery.Append(item.Value == SortOrder.Descending ? $"{item.Key} DESC, " : $"{item.Key} ASC, ");
            }

            // Remove the trailing comma and space
            productQuery.Length -= 2;

            var products = await _context.Products.FromSqlRaw(productQuery.ToString()).ToListAsync(cancellationToken);
            var result = _mapper.Map<List<Response.ProductResponse>>(products);
            return Result.Success(result);
        }
        else // Entity Framework
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
            return Result.Success(result);
        }
    }
}

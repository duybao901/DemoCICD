using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Services.V1.Product;
using DemoCICD.Domain.Abstractions.Repositories;
using DemoCICD.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DemoCICD.Application.UserCases.V1.Queries.Product;
public sealed class GetProductQueryHandler : IQueryHandler<Query.GetProductQuery, PageResult<Response.ProductResponse>>
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

    public async Task<Result<PageResult<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
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

            // Paging
            int pageIndex = request.PageIndex <= 0 ? PageResult<Domain.Entities.Product>.DefaultPageIndex : request.PageIndex;
            int pageSize = request.PageSize <= 0 ? PageResult<Domain.Entities.Product>.DefaultPageSize : request.PageSize;
            if (request.PageSize > PageResult<Domain.Entities.Product>.UpperRangePageSize)
            {
                pageSize = PageResult<Domain.Entities.Product>.UpperRangePageSize;
            }

            productQuery.Append($" OFFSET {pageSize * (pageIndex - 1)} ROWS FETCH NEXT {pageSize} ROWS ONLY");

            var products = await _context.Products.FromSqlRaw(productQuery.ToString()).ToListAsync(cancellationToken);

            int totalCount = await _context.Products.CountAsync(cancellationToken: cancellationToken);

            var productPageResult = PageResult<Domain.Entities.Product>.Create(
                products,
                pageIndex,
                pageSize,
                totalCount);

            var result = _mapper.Map<PageResult<Response.ProductResponse>>(productPageResult);
            return Result.Success(result);
        }
        else // Entity Framework
        {
            IQueryable<Domain.Entities.Product> productQuery = string.IsNullOrEmpty(request.searchTerm) ?
                _productRepositoryBase.FindAll() :
                _productRepositoryBase.FindAll(x => x.Name.Contains(request.searchTerm) || x.Description.Contains(request.searchTerm));

            productQuery = request.SortOrder == SortOrder.Ascending ?
                productQuery.OrderBy(GetSortColumnProterty(request)) :
                productQuery.OrderByDescending(GetSortColumnProterty(request));

            var products = await PageResult<Domain.Entities.Product>.CreateAsync(productQuery,
            request.PageIndex,
            request.PageSize);

            var result = _mapper.Map<PageResult<Response.ProductResponse>>(products);
            return Result.Success(result);
        }
    }

    public static Expression<Func<Domain.Entities.Product, object>> GetSortColumnProterty(Query.GetProductQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "description" => product => product.Description,
            "price" => product => product.Price,
            _ => product => product.Id
            // REAL: product => product.CreatedDate by default
        };
    }
}

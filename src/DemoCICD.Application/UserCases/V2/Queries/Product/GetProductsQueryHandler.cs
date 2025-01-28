using AutoMapper;
using DemoCICD.Contract.Abstractions.Shared;
using DemoCICD.Contract.Services.V2.Product;
using DemoCICD.Domain.Abstractions.Dappers;

namespace DemoCICD.Application.UserCases.V2.Queries.Product;
public sealed class GetProductsQueryHandler : IQueryHandler<Query.GetProductQuery, List<Response.ProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<Response.ProductResponse>>> Handle(Query.GetProductQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();
        var result = _mapper.Map<List<Response.ProductResponse>>(products);
        return Result.Success(result);
    }
}

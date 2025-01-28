using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Abstractions.Dappers.Repositories;

namespace DemoCICD.Infrastructure.Dapper;
public class DapperUnitOfWork : IUnitOfWork
{
    public DapperUnitOfWork(IProductRepository productRepository)
        => ProductRepository = productRepository;

    public IProductRepository ProductRepository { get; }
}

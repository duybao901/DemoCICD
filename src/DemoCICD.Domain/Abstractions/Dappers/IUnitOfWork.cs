using DemoCICD.Domain.Abstractions.Dappers.Repositories;

namespace DemoCICD.Domain.Abstractions.Dappers;
public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
}

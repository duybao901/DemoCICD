namespace DemoCICD.Domain.Abstractions.Dappers.Repositories;
public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id); // If not found, return null

    Task<IReadOnlyList<TEntity>> GetAllAsync();

    // Using this if using Dapper to replace EF
    Task<int> AddAsync(TEntity entity);

    Task<int> UpdateAsync(TEntity entity);

    Task<int> DeleteAsync(Guid id);
}

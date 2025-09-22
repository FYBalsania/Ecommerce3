using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Remove(T entity);
}
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IRepository<T>
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Remove(T entity);
}
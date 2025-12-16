using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal abstract class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AppDbContext _dbContext;
    protected Repository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<T?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
        => trackChanges
            ? await _dbContext.Set<T>().AsTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            : await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
        => await _dbContext.Set<T>().AddAsync(entity, cancellationToken);

    public void Update(T entity) => _dbContext.Set<T>().Update(entity);

    public void Remove(T entity) => _dbContext.Set<T>().Remove(entity);
}
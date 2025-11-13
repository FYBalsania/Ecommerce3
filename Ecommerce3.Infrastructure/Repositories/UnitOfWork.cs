using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        => await _dbContext.SaveChangesAsync(cancellationToken);

    public bool HasChanges() => _dbContext.ChangeTracker.HasChanges();

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        => await _dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        => await _dbContext.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken) 
        => await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
}
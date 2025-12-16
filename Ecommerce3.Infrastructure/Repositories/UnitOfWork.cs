using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
       return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public bool HasChanges() => dbContext.ChangeTracker.HasChanges();

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        => await dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        => await dbContext.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken) 
        => await dbContext.Database.RollbackTransactionAsync(cancellationToken);
}
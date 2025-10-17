namespace Ecommerce3.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken);
    bool HasChanges();
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
}
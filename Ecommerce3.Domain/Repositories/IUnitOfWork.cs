namespace Ecommerce3.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IImageEntityRepository
{
    Type EntityType { get; }
    Type ImageType { get; }
    Task<Entity?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
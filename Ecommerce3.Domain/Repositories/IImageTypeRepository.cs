using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IImageTypeRepository : IRepository<ImageType>
{
    public Task<ImageType?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
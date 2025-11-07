using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandRepository : IEntityWithImagesRepository<Brand, BrandImage> //IRepository<Brand>
{
    public Task<Brand?> GetByIdAsync(int id, BrandInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Brand?> GetByNameAsync(string name, BrandInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}
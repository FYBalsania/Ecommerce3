using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    public Task<(IEnumerable<Brand> Brands, int Count)> GetBrandsAsync(string? name, BrandInclude[] includes,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);

    public Task<Brand?> GetByIdAsync(int id, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Brand?> GetBySlugAsync(string slug, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Brand?> GetByNameAsync(string name, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken);
}
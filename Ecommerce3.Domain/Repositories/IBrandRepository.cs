using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    public Task<(IEnumerable<Brand> Brands, int Count)> GetBrandsAsync(string? name, BrandInclude[] includes, int pageNumber,
        int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Brand?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
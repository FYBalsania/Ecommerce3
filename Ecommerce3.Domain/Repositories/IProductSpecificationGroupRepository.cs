using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductSpecificationGroupRepository : IRepository<ProductSpecificationGroup>
{
    public Task<(IEnumerable<ProductSpecificationGroup> ListItems, int Count)>
        GetProductSpecificationGroupsAsync(string? name, int pageNumber, int pageSize,
            CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<ProductSpecificationGroup?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<ProductSpecificationGroup?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
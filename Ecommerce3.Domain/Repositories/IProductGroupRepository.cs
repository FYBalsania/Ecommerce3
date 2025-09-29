using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductGroupRepository : IRepository<ProductGroup>
{
    public Task<(IReadOnlyCollection<ProductGroup> ListItems, int Count)> GetProductGroupsAsync(
        string? name, ProductGroupInclude[] includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<ProductGroup?> GetByNameAsync(string name, ProductGroupInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductGroup?> GetBySlugAsync(string slug, ProductGroupInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductGroup?> GetByIdAsync(int id, ProductGroupInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken);
}
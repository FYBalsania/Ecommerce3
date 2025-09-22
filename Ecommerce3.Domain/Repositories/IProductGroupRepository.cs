using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductGroupRepository : IRepository<ProductGroup>
{
    public Task<ProductGroup> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<ProductGroup> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);

    public Task<(IReadOnlyCollection<ProductGroupListItem> ListItems, int Count)> GetProductGroupListItemsAsync(
        string? name, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
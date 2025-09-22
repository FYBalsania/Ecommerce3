using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeRepository : IRepository<ProductAttribute>
{
    public Task<ProductAttribute> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<ProductAttribute> GetBySlugAsync(string slug, CancellationToken cancellationToken);

    public Task<(IReadOnlyCollection<ProductAttribute> ListItems, int Count)?>
        GetProductAttributesAsync(string? name, int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
}
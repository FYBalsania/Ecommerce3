using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeRepository : IRepository<ProductAttribute>
{
    public Task<(IReadOnlyCollection<ProductAttribute> ListItems, int Count)?>
        GetProductAttributesAsync(string? name, ProductAttributeInclude includes, bool trackChanges,
            int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);

    public Task<ProductAttribute?> GetBySlugAsync(string slug, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductAttribute?> GetByNameAsync(string name, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductAttribute?> GetByIdAsync(int id, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}
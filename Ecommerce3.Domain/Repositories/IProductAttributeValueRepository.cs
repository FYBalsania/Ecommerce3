using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeValueRepository : IRepository<ProductAttributeValue>
{
    public Task<(IReadOnlyCollection<ProductAttributeValue> ListItems, int Count)> GetProductAttributeValuesAsync(
        string? name, ProductAttributeValueInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);

    public Task<ProductAttributeValue?> GetByNameAsync(string name, ProductAttributeValueInclude includes,
        bool trackChanges, CancellationToken cancellationToken);

    public Task<ProductAttributeValue?> GetBySlugAsync(string slug, ProductAttributeValueInclude includes,
        bool trackChanges, CancellationToken cancellationToken);

    public Task<ProductAttributeValue?> GetByIdAsync(int id, ProductAttributeValueInclude includes,
        bool trackChanges, CancellationToken cancellationToken);
}
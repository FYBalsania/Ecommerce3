using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeValueRepository<T> : IRepository<T> where T : ProductAttributeValue
{
    public Task<(IReadOnlyCollection<T> ListItems, int Count)> GetProductAttributeValuesAsync(
        string? name, ProductAttributeValueInclude include, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<T?> GetByNameAsync(string name, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken);

    public Task<T?> GetBySlugAsync(string slug, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken);

    public Task<T?> GetByIdAsync(int id, ProductAttributeValueInclude include,
        bool trackChanges, CancellationToken cancellationToken);
    // Task<ProductAttributeValue?> GetTextValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeRepository : IRepository<ProductAttribute>
{
    public Task<ProductAttribute?> GetBySlugAsync(string slug, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductAttribute?> GetByNameAsync(string name, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<ProductAttribute?> GetByIdAsync(int id, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
    
    Task<ProductAttributeValue?> GetByProductAttributeValueIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
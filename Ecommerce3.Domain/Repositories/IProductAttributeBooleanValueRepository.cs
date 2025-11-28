using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeBooleanValueRepository : IProductAttributeValueRepository<ProductAttributeBooleanValue>
{
    Task<ProductAttributeBooleanValue?> GetBooleanValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
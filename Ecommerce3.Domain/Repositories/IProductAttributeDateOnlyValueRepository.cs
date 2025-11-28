using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeDateOnlyValueRepository : IProductAttributeValueRepository<ProductAttributeDateOnlyValue>
{
    Task<ProductAttributeDateOnlyValue?> GetDateOnlyValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
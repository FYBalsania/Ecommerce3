using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeDecimalValueRepository : IProductAttributeValueRepository<ProductAttributeDecimalValue>
{
    Task<ProductAttributeDecimalValue?> GetDecimalValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
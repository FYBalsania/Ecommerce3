using Ecommerce3.Contracts.DTOs;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductAttributeValueQueryRepository
{
    Task<bool> ExistsAsync(int id, int productAttributeId, CancellationToken cancellationToken);
    Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
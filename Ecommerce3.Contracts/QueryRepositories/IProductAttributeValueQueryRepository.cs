using Ecommerce3.Contracts.DTOs;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductAttributeValueQueryRepository
{
    Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
}
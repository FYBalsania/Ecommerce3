using Ecommerce3.Contracts.DTO.API.ProductAttribute;

namespace Ecommerce3.Contracts.QueryRepositories.API;

public interface IProductAttributeQueryRepository
{
    Task<IReadOnlyList<ProductAttributeListItemDTO>> GetAllAsync(int? excludeProductGroupId, CancellationToken cancellationToken);
}
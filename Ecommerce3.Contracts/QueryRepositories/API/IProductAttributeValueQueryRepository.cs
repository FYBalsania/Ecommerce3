using Ecommerce3.Contracts.DTO.API.ProductAttributeValue;

namespace Ecommerce3.Contracts.QueryRepositories.API;

public interface IProductAttributeValueQueryRepository
{
    Task<IReadOnlyList<ProductAttributeValueListItemDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken);
}
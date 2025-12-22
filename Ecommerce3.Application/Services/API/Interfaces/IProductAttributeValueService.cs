using Ecommerce3.Contracts.DTO.API.ProductAttributeValue;

namespace Ecommerce3.Application.Services.API.Interfaces;

public interface IProductAttributeValueService
{
    Task<IReadOnlyList<ProductAttributeValueListItemDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken);
}
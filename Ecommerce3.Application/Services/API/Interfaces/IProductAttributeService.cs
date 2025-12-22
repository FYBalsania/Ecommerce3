using Ecommerce3.Contracts.DTO.API.ProductAttribute;

namespace Ecommerce3.Application.Services.API.Interfaces;

public interface IProductAttributeService
{
    Task<IReadOnlyList<ProductAttributeListItemDTO>> GetAllAsync(int? excludeProductGroupId,
        CancellationToken cancellationToken);
}
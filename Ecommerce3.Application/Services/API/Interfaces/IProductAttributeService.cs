using Ecommerce3.Contracts.DTO.API.ProductAttribute;

namespace Ecommerce3.Application.Services.API.Interfaces;

public interface IProductAttributeService
{
    Task<IReadOnlyList<ProductAttributeDTO>> GetAllAsync(int? excludeProductGroupId,
        CancellationToken cancellationToken);
}
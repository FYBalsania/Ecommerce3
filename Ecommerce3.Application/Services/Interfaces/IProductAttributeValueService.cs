using Ecommerce3.Contracts.DTOs;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductAttributeValueService
{
    Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IReadOnlyList<ProductAttributeValueDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken);
}
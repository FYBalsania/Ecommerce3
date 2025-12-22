using Ecommerce3.Application.Services.Interfaces;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Application.Services;

public sealed class ProductAttributeValueService(
    IProductAttributeValueQueryRepository productAttributeValueQueryRepository)
    : IProductAttributeValueService
{
    public async Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await productAttributeValueQueryRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductAttributeValueDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken)
    {
        return await productAttributeValueQueryRepository.GetAllByProductAttributeIdAsync(productAttributeId,
            cancellationToken);
    }
}
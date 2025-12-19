using Ecommerce3.Application.Services.API.Interfaces;
using Ecommerce3.Contracts.DTO.API.ProductAttributeValue;
using Ecommerce3.Contracts.QueryRepositories.API;

namespace Ecommerce3.Application.Services.API;

internal sealed class ProductAttributeValueService(
    IProductAttributeValueQueryRepository productAttributeValueQueryRepository)
    : IProductAttributeValueService
{
    public async Task<IReadOnlyList<ProductAttributeValueDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken)
    {
        return await productAttributeValueQueryRepository.GetAllByProductAttributeIdAsync(productAttributeId, cancellationToken);
    }
}
using Ecommerce3.Application.Services.API.Interfaces;
using Ecommerce3.Contracts.DTO.API.ProductAttribute;
using Ecommerce3.Contracts.QueryRepositories.API;

namespace Ecommerce3.Application.Services.API;

internal sealed class ProductAttributeService(IProductAttributeQueryRepository productAttributeQueryRepository)
    : IProductAttributeService
{
    public async Task<IReadOnlyList<ProductAttributeListItemDTO>> GetAllAsync(int? excludeProductGroupId,
        CancellationToken cancellationToken)
    {
        return await productAttributeQueryRepository.GetAllAsync(excludeProductGroupId, cancellationToken);
    }
}
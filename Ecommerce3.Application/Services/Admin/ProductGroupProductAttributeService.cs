using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Contracts.DTO.Admin.ProductGroup;
using Ecommerce3.Contracts.QueryRepositories.Admin;

namespace Ecommerce3.Application.Services.Admin;

internal sealed class ProductGroupProductAttributeService(IProductGroupProductAttributeQueryRepository queryRepository)
    : IProductGroupProductAttributeService
{
    public async Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken)
    {
        return await queryRepository.GetMaxSortOrderAsync(productGroupId, cancellationToken);
    }

    public async Task<ProductAttributeEditDTO?> GetByParamsAsync(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken)
    {
        return await queryRepository.GetByParamsAsync(productGroupId, productAttributeId, cancellationToken);
    }
}
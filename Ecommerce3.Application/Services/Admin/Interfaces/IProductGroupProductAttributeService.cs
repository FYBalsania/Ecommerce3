using Ecommerce3.Contracts.DTO.Admin.ProductGroup;

namespace Ecommerce3.Application.Services.Admin.Interfaces;

public interface IProductGroupProductAttributeService
{
    Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken);

    Task<ProductAttributeEditDTO?> GetByParamsAsync(int productGroupId,
        int productAttributeId, CancellationToken cancellationToken);
}
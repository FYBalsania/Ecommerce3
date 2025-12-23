using Ecommerce3.Contracts.DTO.Admin.ProductGroup;

namespace Ecommerce3.Contracts.QueryRepositories.Admin;

public interface IProductGroupProductAttributeQueryRepository
{
    Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken);

    Task<ProductAttributeEditDTO?> GetByParamsAsync(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken);
}
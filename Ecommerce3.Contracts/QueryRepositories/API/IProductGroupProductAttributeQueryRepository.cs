using Ecommerce3.Contracts.DTO.API.ProductGroup;

namespace Ecommerce3.Contracts.QueryRepositories.API;

public interface IProductGroupProductAttributeQueryRepository
{
    Task<ProductGroupProductAttributeViewDTO?> GetAsync(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken);
}
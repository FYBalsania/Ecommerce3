using Ecommerce3.Contracts.QueryRepositories;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductAttributeQueryRepository : IProductAttributeQueryRepository
{
    public ProductAttributeQueryRepository()
    {
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
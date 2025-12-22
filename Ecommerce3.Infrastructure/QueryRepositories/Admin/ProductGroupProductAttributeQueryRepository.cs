using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.Admin;

internal sealed class ProductGroupProductAttributeQueryRepository(AppDbContext dbContext)
    : IProductGroupProductAttributeQueryRepository
{
    public async Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductGroupProductAttributes
            .Where(x => x.ProductGroupId == productGroupId)
            .Select(x => (decimal?)x.ProductAttributeSortOrder)
            .MaxAsync(cancellationToken) ?? 0m;
    }
}
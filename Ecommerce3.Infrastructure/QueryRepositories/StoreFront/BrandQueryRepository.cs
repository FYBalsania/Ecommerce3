using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal sealed class BrandQueryRepository(AppDbContext dbContext) : IBrandQueryRepository
{
    public async Task<IReadOnlyDictionary<int, string>> GetIdAndDisplayByCategoryIdsAsync(int[] categoryIds,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductCategories
            .Where(pc => ((IEnumerable<int>)categoryIds).Contains(pc.CategoryId)
                         && pc.Category!.IsActive
                         && pc.Product!.Brand!.IsActive
                         && pc.Product.Status == ProductStatus.Active)
            .Distinct()
            .ToDictionaryAsync(x => x.Product!.Brand!.Id, x => x.Product!.Brand!.Display, cancellationToken);
    }
}
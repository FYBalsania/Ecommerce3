using Ecommerce3.Contracts.DTO.StoreFront.Brand;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal sealed class BrandQueryRepository(AppDbContext dbContext) : IBrandQueryRepository
{
    public async Task<IReadOnlyList<BrandCheckBoxListItemDTO>> GetByCategoryIdsAsync(int[] categoryIds,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductCategories
            .Where(pc => ((IEnumerable<int>)categoryIds).Contains(pc.CategoryId) 
                         && pc.Product!.Brand!.IsActive
                         && pc.Product.Status == ProductStatus.Active)
            .Select(pc => new BrandCheckBoxListItemDTO
            {
                Id = pc.Product!.BrandId,
                Name = pc.Product.Brand!.Name,
                Display = pc.Product.Brand.Display,
                Slug = pc.Product.Brand.Slug
            })
            .Distinct()
            .ToListAsync(cancellationToken);
    }
}
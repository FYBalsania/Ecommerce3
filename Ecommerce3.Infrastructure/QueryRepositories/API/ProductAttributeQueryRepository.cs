using Ecommerce3.Contracts.DTO.API.ProductAttribute;
using Ecommerce3.Contracts.QueryRepositories.API;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.API;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.API;

internal sealed class ProductAttributeQueryRepository(AppDbContext dbContext) : IProductAttributeQueryRepository
{
    public async Task<IReadOnlyList<ProductAttributeListItemDTO>> GetAllAsync(int? excludeProductGroupId,
        CancellationToken cancellationToken)
    {
        var query = dbContext.ProductAttributes.AsQueryable();

        if (excludeProductGroupId.HasValue)
        {
            query = query.Where(a => !dbContext.ProductGroupProductAttributes
                .Any(b => b.ProductGroupId == excludeProductGroupId.Value 
                          && b.ProductAttributeId == a.Id));
        }

        return await query
            .Select(ProductAttributeExpressions.DTOExpression)
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);
    }
}
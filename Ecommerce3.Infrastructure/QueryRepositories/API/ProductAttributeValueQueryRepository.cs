using Ecommerce3.Contracts.DTO.API.ProductAttributeValue;
using Ecommerce3.Contracts.QueryRepositories.API;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.API;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.API;

internal class ProductAttributeValueQueryRepository(AppDbContext dbContext)
    : IProductAttributeValueQueryRepository
{
    public async Task<IReadOnlyList<ProductAttributeValueListItemDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductAttributeValues
            .Where(x => x.ProductAttributeId == productAttributeId)
            .OrderBy(x => x.Value)
            .Select(ProductAttributeValueExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }
}
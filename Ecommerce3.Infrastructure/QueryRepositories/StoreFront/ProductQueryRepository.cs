using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.StoreFront;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal class ProductQueryRepository(AppDbContext dbContext) : IProductQueryRepository
{
    public async Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku,
        CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .Where(x => ((IEnumerable<string>)sku).Contains(x.SKU))
            .AsSplitQuery()
            .Select(ProductExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }
}
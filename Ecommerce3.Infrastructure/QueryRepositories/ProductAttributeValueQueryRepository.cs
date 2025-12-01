using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductAttributeValueQueryRepository : IProductAttributeValueQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeValueQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductAttributeValues
            .Where(x => x.Id == id)
            .Select(x => new ProductAttributeValueDTO(
                x.Id, x.Value, x.Slug, x.Display, x.Breadcrumb, x.SortOrder, x.CreatedByUser!.FullName,
                x.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
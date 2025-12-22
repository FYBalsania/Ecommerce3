using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ProductAttributeValueQueryRepository(AppDbContext dbContext) : IProductAttributeValueQueryRepository
{
    public async Task<bool> ExistsAsync(int id, int productAttributeId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductAttributeValues
            .AnyAsync(x => x.Id == id && x.ProductAttributeId == productAttributeId, cancellationToken);
    }

    public async Task<ProductAttributeValueDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.ProductAttributeValues
            .Where(x => x.Id == id)
            .Include(x => x.CreatedByUser)
            .Select(x => x.ToDTO())
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductAttributeValueDTO>> GetAllByProductAttributeIdAsync(int productAttributeId,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductAttributeValues
            .Where(x => x.ProductAttributeId == productAttributeId)
            .Include(x => x.CreatedByUser)
            .Select(x => x.ToDTO())
            .ToListAsync(cancellationToken);
    }
}
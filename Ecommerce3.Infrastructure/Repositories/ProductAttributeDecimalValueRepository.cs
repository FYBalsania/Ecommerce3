using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeDecimalValueRepository : ProductAttributeValueRepository<ProductAttributeDecimalValue>,
    IProductAttributeDecimalValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeDecimalValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProductAttributeDecimalValue?> GetDecimalValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributeDecimalValues.AsTracking()
            : _dbContext.ProductAttributeDecimalValues.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeDateOnlyValueRepository : ProductAttributeValueRepository<ProductAttributeDateOnlyValue>,
    IProductAttributeDateOnlyValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeDateOnlyValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProductAttributeDateOnlyValue?> GetDateOnlyValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributeDateOnlyValues.AsTracking()
            : _dbContext.ProductAttributeDateOnlyValues.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
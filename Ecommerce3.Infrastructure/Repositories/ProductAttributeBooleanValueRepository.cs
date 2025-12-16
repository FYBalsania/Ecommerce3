using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeBooleanValueRepository : ProductAttributeValueRepository<ProductAttributeBooleanValue>,
    IProductAttributeBooleanValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeBooleanValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProductAttributeBooleanValue?> GetBooleanValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributeBooleanValues.AsTracking()
            : _dbContext.ProductAttributeBooleanValues.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
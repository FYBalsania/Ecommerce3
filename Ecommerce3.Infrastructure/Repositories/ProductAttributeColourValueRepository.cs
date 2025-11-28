using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeColourValueRepository : ProductAttributeValueRepository<ProductAttributeColourValue>,
    IProductAttributeColourValueRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeColourValueRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IReadOnlyCollection<ProductAttributeColourValue> ListItems, int Count)>
        GetProductAttributeColourValuesAsync(string? name, ProductAttributeColourValueInclude includes,
            bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<ProductAttributeColourValue?> GetColourValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributeColourValues.AsQueryable()
            : _dbContext.ProductAttributeColourValues.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<ProductAttributeValue?> GetTextValueByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributeValues.AsQueryable()
            : _dbContext.ProductAttributeValues.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
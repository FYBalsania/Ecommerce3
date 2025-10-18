using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<ProductAttribute> GetQuery(ProductAttributeInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.ProductAttributes.AsQueryable()
            : _dbContext.ProductAttributes.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing).
        if ((includes & ProductAttributeInclude.Values) == ProductAttributeInclude.Values)
            query = query.Include(x => x.Values);

        if ((includes & ProductAttributeInclude.CreatedByUser) == ProductAttributeInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);

        if ((includes & ProductAttributeInclude.UpdatedByUser) == ProductAttributeInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);

        if ((includes & ProductAttributeInclude.DeletedByUser) == ProductAttributeInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<ProductAttribute?> GetBySlugAsync(string slug, ProductAttributeInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductAttribute?> GetByNameAsync(string name, ProductAttributeInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductAttribute?> GetByIdAsync(int id, ProductAttributeInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
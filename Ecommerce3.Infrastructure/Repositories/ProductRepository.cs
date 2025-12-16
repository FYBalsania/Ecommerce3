using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductRepository(AppDbContext dbContext) 
    : EntityWithImagesRepository<Product, ProductImage>(dbContext), IProductRepository
{
    private readonly AppDbContext _dbContext1 = dbContext;

    private IQueryable<Product> GetQuery(ProductInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext1.Products.AsTracking()
            : _dbContext1.Products.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & ProductInclude.Images) == ProductInclude.Images) 
            query = query.Include(x => x.Images);
        if ((includes & ProductInclude.Brand) == ProductInclude.Brand) 
            query = query.Include(x => x.Brand);
        if ((includes & ProductInclude.ProductGroup) == ProductInclude.ProductGroup) 
            query = query.Include(x => x.ProductGroup);
        if ((includes & ProductInclude.DeliveryWindow) == ProductInclude.DeliveryWindow) 
            query = query.Include(x => x.DeliveryWindow);
        if ((includes & ProductInclude.CreatedByUser) == ProductInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & ProductInclude.UpdatedByUser) == ProductInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & ProductInclude.DeletedByUser) == ProductInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);
        if ((includes & ProductInclude.Categories) == ProductInclude.Categories) 
            query = query.Include(x => x.Categories);
        if ((includes & ProductInclude.TextListItems) == ProductInclude.TextListItems) 
            query = query.Include(x => x.TextListItems);
        if ((includes & ProductInclude.KVPListItems) == ProductInclude.KVPListItems) 
            query = query.Include(x => x.KVPListItems);
        if ((includes & ProductInclude.QnAs) == ProductInclude.QnAs) 
            query = query.Include(x => x.QnAs);
        if ((includes & ProductInclude.Reviews) == ProductInclude.Reviews) 
            query = query.Include(x => x.Reviews);
        if ((includes & ProductInclude.Attributes) == ProductInclude.Attributes) 
            query = query.Include(x => x.Attributes);
        if ((includes & ProductInclude.UnitOfMeasure) == ProductInclude.UnitOfMeasure) 
            query = query.Include(x => x.UnitOfMeasure);
        
        return query;
    }

    public async Task<Product?> GetByIdAsync(int id, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        return await GetQuery(includes, trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
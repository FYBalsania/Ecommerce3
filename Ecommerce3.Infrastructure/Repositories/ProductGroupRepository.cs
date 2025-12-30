using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductGroupRepository(AppDbContext dbContext)
    : EntityWithImagesRepository<ProductGroup, ProductGroupImage>(dbContext),
        IProductGroupRepository
{
    private IQueryable<ProductGroup> GetQuery(ProductGroupInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? dbContext.ProductGroups.AsTracking()
            : dbContext.ProductGroups.AsNoTracking();

        if ((includes & ProductGroupInclude.Images) == ProductGroupInclude.Images)
            query = query.Include(x => x.Images);
        if ((includes & ProductGroupInclude.Attributes) == ProductGroupInclude.Attributes)
            query = query.Include(x => x.Attributes)
                .ThenInclude(x => x.ProductAttribute);
        if ((includes & ProductGroupInclude.AttributeValues) == ProductGroupInclude.AttributeValues)
            query = query.Include(x => x.Attributes)
                .ThenInclude(x => x.ProductAttributeValue);
        if ((includes & ProductGroupInclude.CreatedByUser) == ProductGroupInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & ProductGroupInclude.UpdatedByUser) == ProductGroupInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & ProductGroupInclude.DeletedByUser) == ProductGroupInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<ProductGroup?> GetByNameAsync(string name, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<ProductGroup?> GetBySlugAsync(string slug, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<ProductGroup?> GetByIdAsync(int id, ProductGroupInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
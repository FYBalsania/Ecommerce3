using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductGroupPageRepository : PageRepository<ProductGroupPage>, IProductGroupPageRepository
{
    private readonly AppDbContext _dbContext;

    public ProductGroupPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductGroupPage?> GetByProductGroupIdAsync(int productGroupId, ProductGroupPageInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductGroupPages.Where(x => x.ProductGroupId == productGroupId).AsQueryable()
            : _dbContext.ProductGroupPages.Where(x => x.ProductGroupId == productGroupId).AsNoTracking();

        if ((includes & ProductGroupPageInclude.ProductGroup) == ProductGroupPageInclude.ProductGroup) 
            query = query.Include(x => x.Images);
        if ((includes & ProductGroupPageInclude.CreatedByUser) == ProductGroupPageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & ProductGroupPageInclude.UpdatedByUser) == ProductGroupPageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & ProductGroupPageInclude.DeletedByUser) == ProductGroupPageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
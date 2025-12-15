using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductPageRepository(AppDbContext dbContext) : PageRepository<ProductPage>(dbContext), IProductPageRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<ProductPage?> GetByProductIdAsync(int productId, ProductPageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.ProductPages.Where(x => x.ProductId == productId).AsQueryable()
            : _dbContext.ProductPages.Where(x => x.ProductId == productId).AsNoTracking();

        if ((includes & ProductPageInclude.Product) == ProductPageInclude.Product) 
            query = query.Include(x => x.Product);
        if ((includes & ProductPageInclude.CreatedByUser) == ProductPageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & ProductPageInclude.UpdatedByUser) == ProductPageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & ProductPageInclude.DeletedByUser) == ProductPageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
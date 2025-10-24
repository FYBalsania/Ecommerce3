using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandPageRepository : PageRepository<BrandPage>, IBrandPageRepository
{
    private readonly AppDbContext _dbContext;

    public BrandPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BrandPage?> GetByBrandIdAsync(int brandId, BrandPageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.BrandPages.Where(x => x.BrandId == brandId).AsQueryable()
            : _dbContext.BrandPages.Where(x => x.BrandId == brandId).AsNoTracking();

        if ((includes & BrandPageInclude.Brand) == BrandPageInclude.Brand) 
            query = query.Include(x => x.Images);
        if ((includes & BrandPageInclude.CreatedByUser) == BrandPageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & BrandPageInclude.UpdatedByUser) == BrandPageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & BrandPageInclude.DeletedByUser) == BrandPageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
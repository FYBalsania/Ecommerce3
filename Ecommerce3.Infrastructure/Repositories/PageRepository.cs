using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class PageRepository : Repository<Page>, IPageRepository
{
    public PageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IQueryable<Page> GetQuery(PageInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Pages.AsQueryable()
            : _dbContext.Pages.AsNoTracking();

        if ((includes & PageInclude.CreatedByUser) == PageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & PageInclude.UpdatedByUser) == PageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & PageInclude.DeletedByUser) == PageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<(IEnumerable<Page> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        string? canonicalUrl, int? seoScore, PageInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByPathAsync(string path, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Page?> GetByPathAsync(string path, PageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Page?> GetByIdAsync(int id, PageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByBrandIdAsync(int brandId, CancellationToken cancellationToken)
        => await _dbContext.Pages.IgnoreQueryFilters().AnyAsync(x => x.BrandId == brandId, cancellationToken);

    public async Task<Page?> GetByBrandIdAsync(int brandId, PageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.BrandId == brandId, cancellationToken);
    }
}
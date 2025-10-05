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
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class PageRepository<T> : EntityWithImagesRepository<T, PageImage>, IPageRepository<T> where T : Page
{
    private readonly AppDbContext _dbContext;

    public PageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<T> GetQuery(PageInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Set<T>().AsTracking()
            : _dbContext.Set<T>().AsNoTracking();

        if ((includes & PageInclude.CreatedByUser) == PageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & PageInclude.UpdatedByUser) == PageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & PageInclude.DeletedByUser) == PageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<(IEnumerable<T> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        string? canonicalUrl, int? seoScore, PageInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByPathAsync(string path, PageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<T?> GetByIdAsync(int id, PageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class PageRepository : Repository<Page>, IPageRepository
{
    private readonly AppDbContext _dbContext;

    public PageRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Page> ListItems, int Count)?> GetPagesAsync(string? path, string? title,
        int pageNumber, string? canonicalUrl, int? seoScore, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByPathAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Page?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
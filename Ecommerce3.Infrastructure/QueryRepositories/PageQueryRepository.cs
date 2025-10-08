using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class PageQueryRepository : IPageQueryRepository
{
    private readonly AppDbContext _dbContext;

    public PageQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Pages.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.H1, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var pages = await query
            .OrderBy(b => b.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PageListItemDTO(x.Id, x.Discriminator, x.Path, x.BrandId, x.CategoryId, x.ProductId, x.ProductGroupId , x.IsActive, x.CreatedByUser!.FullName, x.CreatedAt))
            .ToListAsync(cancellationToken);

        return (pages, total);
    }
}
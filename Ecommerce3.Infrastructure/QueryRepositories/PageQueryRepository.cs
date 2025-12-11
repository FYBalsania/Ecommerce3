using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class PageQueryRepository(AppDbContext dbContext) : IPageQueryRepository
{
    public async Task<(IReadOnlyList<PageListItemDTO>, int)> GetPageListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Pages.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.H1, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var pages = await query
            .OrderBy(b => b.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PageListItemDTO(x.Id, "", x.Path, x.BrandId, x.CategoryId, x.ProductId, x.ProductGroupId , x.IsActive, x.CreatedByUser!.FullName, x.CreatedAt))
            .ToListAsync(cancellationToken);

        return (pages, total);
    }

    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        return await dbContext.Pages
            .Where(x => x.Path == path && x.IsActive)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    }
    
}
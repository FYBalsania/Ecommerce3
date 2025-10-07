using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class BrandQueryRepository : IBrandQueryRepository
{
    private readonly AppDbContext _dbContext;

    public BrandQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(IReadOnlyList<BrandListItemDTO>, int)> GetBrandListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Brands.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.Name, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var brands = await query
            .OrderBy(b => b.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new BrandListItemDTO(x.Id, x.Name, x.Slug, x.SortOrder, x.CreatedByUser!.FullName, x.CreatedAt))
            .ToListAsync(cancellationToken);

        return (brands, total);
    }
}
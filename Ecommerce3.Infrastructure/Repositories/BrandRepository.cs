using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandRepository : Repository<Brand>, IBrandRepository, IBrandQueryRepository
{
    public BrandRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    private IQueryable<Brand> GetQuery(BrandInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Brands.AsQueryable()
            : _dbContext.Brands.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & BrandInclude.Images) == BrandInclude.Images) query = query.Include(x => x.Images);
        if ((includes & BrandInclude.CreatedUser) == BrandInclude.CreatedUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & BrandInclude.UpdatedUser) == BrandInclude.UpdatedUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & BrandInclude.DeletedUser) == BrandInclude.DeletedUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<Brand?> GetByIdAsync(int id, BrandInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Brand?> GetBySlugAsync(string slug, BrandInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Brand?> GetByNameAsync(string name, BrandInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = GetQuery(BrandInclude.None, false);

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = GetQuery(BrandInclude.None, false);

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<(IReadOnlyList<BrandListItemDTO>, int)> GetBrandListItemsAsync(string? name, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = GetQuery(BrandInclude.CreatedUser, false);

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.Name, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var brands = await query
            .OrderBy(b => b.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new BrandListItemDTO(x.Id, x.Name, x.Slug, x.SortOrder, x.IsActive, x.Images.Count, x.CreatedByUser!.FullName, x.CreatedAt))
            .ToListAsync(cancellationToken);

        return (brands, total);
    }
}
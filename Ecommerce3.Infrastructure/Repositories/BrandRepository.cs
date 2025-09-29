using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandRepository : Repository<Brand>, IBrandRepository
{
    private readonly AppDbContext _dbContext;

    public BrandRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    private IQueryable<Brand> GetQuery(BrandInclude[] includes, bool trackChanges)
    {
        var query = _dbContext.Brands.AsQueryable();
        if (!trackChanges) query = query.AsNoTracking();
        if (includes.Contains(BrandInclude.Images)) query.Include(x => x.Images);
        if (includes.Contains(BrandInclude.CreatedUser)) query.Include(x => x.CreatedByUser);
        if (includes.Contains(BrandInclude.UpdatedUser)) query.Include(x => x.UpdatedByUser);
        if (includes.Contains(BrandInclude.DeletedUser)) query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<(IEnumerable<Brand> Brands, int Count)> GetBrandsAsync(string? name, BrandInclude[] includes,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, false);

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(b => EF.Functions.Like(b.Name, $"%{name}%"));

        var total = await query.CountAsync(cancellationToken);
        var brands = await query
            .OrderBy(b => b.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync(cancellationToken);

        return (brands, total);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Slug == slug, cancellationToken);

    public async Task<Brand?> GetByIdAsync(int id, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Brand?> GetBySlugAsync(string slug, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Brand?> GetByNameAsync(string name, BrandInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Name == name && x.Id != excludeId, cancellationToken);

    public async Task<bool> ExistsBySlugAsync(string slug, int excludeId, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Slug == slug && x.Id != excludeId, cancellationToken);
}
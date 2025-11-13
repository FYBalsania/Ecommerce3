using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class BrandRepository : EntityWithImagesRepository<Brand, BrandImage>, IBrandRepository
{
    private readonly AppDbContext _dbContext;

    public BrandRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
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
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _dbContext;
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Category> GetQuery(CategoryInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Categories.AsQueryable()
            : _dbContext.Categories.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & CategoryInclude.Images) == CategoryInclude.Images) query = query.Include(x => x.Images);
        if ((includes & CategoryInclude.CreatedByUser) == CategoryInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & CategoryInclude.UpdatedByUser) == CategoryInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & CategoryInclude.DeletedByUser) == CategoryInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }
    
    public async Task<(IEnumerable<Category> ListItems, int Count)> GetCategoriesAsync(string? name, int parentId,
        CategoryInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByIdAsync(int id, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Category?> GetBySlugAsync(string slug, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}
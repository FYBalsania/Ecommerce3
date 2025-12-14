using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CategoryPageRepository : PageRepository<CategoryPage>, ICategoryPageRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CategoryPage?> GetByCategoryIdAsync(int categoryId, CategoryPageInclude includes,
        bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.CategoryPages.Where(x => x.CategoryId == categoryId).AsQueryable()
            : _dbContext.CategoryPages.Where(x => x.CategoryId == categoryId).AsNoTracking();

        if ((includes & CategoryPageInclude.Category) == CategoryPageInclude.Category) 
            query = query.Include(x => x.Images);
        if ((includes & CategoryPageInclude.CreatedByUser) == CategoryPageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & CategoryPageInclude.UpdatedByUser) == CategoryPageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & CategoryPageInclude.DeletedByUser) == CategoryPageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
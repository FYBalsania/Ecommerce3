using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CategoryRepository : EntityWithImagesRepository<Category, CategoryImage>, ICategoryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<Category> Query(CategoryInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Categories.AsTracking().AsQueryable()
            : _dbContext.Categories.AsNoTracking().AsQueryable();

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

    public async Task<Category?> GetByIdAsync(int id, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Query(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Category?> GetBySlugAsync(string slug, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Query(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CategoryInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = Query(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<List<Category>> GetDescendantsAsync(int id, CategoryInclude include, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var parent = await GetByIdAsync(id, CategoryInclude.None, false, cancellationToken);
        return await Query(include, trackChanges).Where(x => x.Path.IsDescendantOf(parent!.Path))
            .OrderBy(x => x.Path.NLevel)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateDescendantPathsAsync(int categoryId, string oldPath, string newPath,
        CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
            sql: @"
        UPDATE ""Category""
        SET ""Path"" = @newPath || subpath(""Path"", nlevel(@oldPath))
        WHERE ""Path"" @> @oldPath
          AND NOT (@newPath <@ ""Path"");",
            parameters: new[]
            {
                new NpgsqlParameter("@oldPath", NpgsqlDbType.LTree) { Value = oldPath },
                new NpgsqlParameter("@newPath", NpgsqlDbType.LTree) { Value = newPath }
            },
            cancellationToken: cancellationToken
        );

    }
}
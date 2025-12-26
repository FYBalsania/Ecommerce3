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
            ? _dbContext.Categories.AsTracking()
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

    public async Task UpdateDescendantPathsAsync(string oldPath, string newPath,
        CancellationToken cancellationToken)
    {
        var sql = @"
                WITH p AS (
                    SELECT @old_path::ltree AS old_path,
                           @new_path::ltree AS new_path
                )
                UPDATE ""Category"" c
                SET ""Path"" =
                    CASE
                        WHEN c.""Path"" = p.old_path
                            THEN p.new_path
                        ELSE p.new_path
                             || subpath(
                                    c.""Path"",
                                    nlevel(p.old_path),
                                    nlevel(c.""Path"") - nlevel(p.old_path)
                                )
                    END
                FROM p
                WHERE c.""Path"" <@ p.old_path;";

        var parameters = new List<NpgsqlParameter>();
        parameters.Add(
            new NpgsqlParameter
            {
                ParameterName = "old_path",
                NpgsqlDbType = NpgsqlDbType.LTree,
                Value = oldPath
            });
        parameters.Add(
            new NpgsqlParameter
            {
                ParameterName = "new_path",
                NpgsqlDbType = NpgsqlDbType.LTree,
                Value = newPath
            });

        await _dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Category>> GetByIdAsync(IEnumerable<int> ids, CategoryInclude include,
        bool trackChanges, CancellationToken cancellationToken)
    {
        return await Query(include, trackChanges).Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }
}
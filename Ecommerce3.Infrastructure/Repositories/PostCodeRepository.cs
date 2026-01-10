using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class PostCodeRepository(AppDbContext dbContext) : Repository<PostCode>(dbContext), IPostCodeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    private IQueryable<PostCode> GetQuery(PostCodeInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.PostCodes.AsTracking()
            : _dbContext.PostCodes.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & PostCodeInclude.CreatedUser) == PostCodeInclude.CreatedUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & PostCodeInclude.UpdatedUser) == PostCodeInclude.UpdatedUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & PostCodeInclude.DeletedUser) == PostCodeInclude.DeletedUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<PostCode?> GetByIdAsync(int id, PostCodeInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PostCode?> GetByCodeAsync(string code, PostCodeInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
    }
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class BankRepository : EntityWithImagesRepository<Bank, BankImage>, IBankRepository
{
    private readonly AppDbContext _dbContext;

    public BankRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    private IQueryable<Bank> GetQuery(BankInclude includes, bool trackChanges)
    {
        var query = trackChanges
            ? _dbContext.Banks.AsQueryable()
            : _dbContext.Banks.AsNoTracking();

        // Use bitwise checks (avoid Enum.HasFlag boxing)
        if ((includes & BankInclude.CreatedUser) == BankInclude.CreatedUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & BankInclude.UpdatedUser) == BankInclude.UpdatedUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & BankInclude.DeletedUser) == BankInclude.DeletedUser)
            query = query.Include(x => x.DeletedByUser);

        return query;
    }

    public async Task<Bank?> GetByIdAsync(int id, BankInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Bank?> GetBySlugAsync(string slug, BankInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<Bank?> GetByNameAsync(string name, BankInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = GetQuery(includes, trackChanges);
        return await query.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}
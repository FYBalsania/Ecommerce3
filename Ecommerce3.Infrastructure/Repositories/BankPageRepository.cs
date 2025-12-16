using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class BankPageRepository(AppDbContext dbContext) : PageRepository<BankPage>(dbContext), IBankPageRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<BankPage?> GetByBankIdAsync(int bankId, BankPageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.BankPages.Where(x => x.BankId == bankId).AsTracking()
            : _dbContext.BankPages.Where(x => x.BankId == bankId).AsNoTracking();

        if ((includes & BankPageInclude.Bank) == BankPageInclude.Bank) 
            query = query.Include(x => x.Bank);
        if ((includes & BankPageInclude.CreatedByUser) == BankPageInclude.CreatedByUser)
            query = query.Include(x => x.CreatedByUser);
        if ((includes & BankPageInclude.UpdatedByUser) == BankPageInclude.UpdatedByUser)
            query = query.Include(x => x.UpdatedByUser);
        if ((includes & BankPageInclude.DeletedByUser) == BankPageInclude.DeletedByUser)
            query = query.Include(x => x.DeletedByUser);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
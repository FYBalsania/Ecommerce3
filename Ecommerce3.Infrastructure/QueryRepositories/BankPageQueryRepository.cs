using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class BankPageQueryRepository(AppDbContext dbContext) : PageQueryRepository(dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;
    
    public override Type PageType => typeof(BankPage);

    public override async Task<PageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.BankPages
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
}
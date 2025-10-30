using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BankPageRepository : PageRepository<BankPage>, IBankPageRepository
{
    private readonly AppDbContext _dbContext;

    public BankPageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
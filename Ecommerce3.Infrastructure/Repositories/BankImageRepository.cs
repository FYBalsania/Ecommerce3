using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BankImageRepository : ImageRepository<BankImage>, IBankImageRepository
{
    private readonly AppDbContext _dbContext;

    public BankImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
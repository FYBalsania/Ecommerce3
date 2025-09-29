using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class DiscountProductRepository : IDiscountProductRepository
{
    private readonly AppDbContext _dbContext;

    public DiscountProductRepository(AppDbContext dbContext) => _dbContext = dbContext;
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CartLineRepository : Repository<CartLine>, ICartLineRepository
{
    private readonly AppDbContext _dbContext;

    public CartLineRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;
}
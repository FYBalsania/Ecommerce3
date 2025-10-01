using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandPageRepository : Repository<BrandPage>, IBrandPageRepository
{
    public BrandPageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
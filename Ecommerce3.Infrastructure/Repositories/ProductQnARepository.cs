using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class ProductQnARepository : Repository<ProductQnA>, IProductQnARepository
{
    public ProductQnARepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
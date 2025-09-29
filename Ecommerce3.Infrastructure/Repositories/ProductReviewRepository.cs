using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductReviewRepository : Repository<ProductReview>, IProductReviewRepository
{
    private readonly AppDbContext _dbContext;

    public ProductReviewRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;
}
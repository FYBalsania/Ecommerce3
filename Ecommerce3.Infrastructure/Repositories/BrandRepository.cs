using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class BrandRepository : Repository<Brand>, IBrandRepository
{
    private readonly AppDbContext _dbContext;

    public BrandRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public async Task<(IEnumerable<BrandListItem>, int Count)> GetBrandListItemsByNameAsync(string? name,
        int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
        => await _dbContext.Brands.AnyAsync(x => x.Slug == slug, cancellationToken);

    public Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
        => _dbContext.Brands.FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);

    public Task<Brand?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => _dbContext.Brands.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
}
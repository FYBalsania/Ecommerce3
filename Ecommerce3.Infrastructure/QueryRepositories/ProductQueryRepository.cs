using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.Product;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ProductQueryRepository(AppDbContext dbContext) : IProductQueryRepository
{
    public async Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(ProductFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (!string.IsNullOrWhiteSpace(filter.Display))
            query = query.Where(x => x.Display.Contains(filter.Display));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));
        if (!string.IsNullOrWhiteSpace(filter.Brand))
            query = query.Where(x => x.Brand.Name.Contains(filter.Brand));
        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(x => x.Categories.Any(c => c.Category.Name.Contains(filter.Category)));

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductListItemDTO>()
        {
            Data = products,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<decimal> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.Products.MaxAsync(x => (decimal?)x.SortOrder, cancellationToken) ?? 0m;

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
        => await dbContext.Products.AnyAsync(x => (excludeId == null || x.Id != excludeId) && x.Name == name, cancellationToken);

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
        => await dbContext.Products.AnyAsync(x => (excludeId == null || x.Id != excludeId) && x.Slug == slug, cancellationToken);

    public async Task<bool> ExistsBySKUAsync(string sku, int? excludeId, CancellationToken cancellationToken) 
        => await dbContext.Products.AnyAsync(x => (excludeId == null || x.Id != excludeId) && x.SKU == sku, cancellationToken);

    public async Task<ProductDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Products
            .Where(x => x.Id == id)
            .Select(ProductExtensions.DTOExpression)
            .FirstOrDefaultAsync(cancellationToken);
}
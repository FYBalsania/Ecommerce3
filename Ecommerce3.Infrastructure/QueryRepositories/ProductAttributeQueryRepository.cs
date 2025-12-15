using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ProductAttributeQueryRepository(AppDbContext dbContext) : IProductAttributeQueryRepository
{
    public async Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter,
        int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = dbContext.ProductAttributes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (!string.IsNullOrWhiteSpace(filter.Display))
            query = query.Where(x => x.Display.Contains(filter.Display));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var productAttributes = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductAttributeListItemDTO>()
        {
            Data = productAttributes,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.ProductAttributes.Select(x => (int?)x.SortOrder).MaxAsync(cancellationToken) ?? 0;

    public async Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var productAttribute = await dbContext.ProductAttributes
            .Include(x => x.Values)
                .ThenInclude(x => x.CreatedByUser)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (productAttribute == null) return null;

        return new ProductAttributeDTO();
    }

    public async Task<ProductAttributeValueDTO?> GetValueByProductAttributeValueIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ProductAttributeValues
            .Include(x => x.CreatedByUser)
            .Select(x => x.ToDTO())
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ProductAttributeValueDTO>> GetValuesByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ProductAttributeValues
            .Where(x => x.ProductAttributeId == id)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Value)
            .Include(x => x.CreatedByUser)
            .Select(x => x.ToDTO())
            .ToListAsync(cancellationToken);
}
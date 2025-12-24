using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.ProductGroup;
using Ecommerce3.Contracts.DTO.Admin.ProductGroupProductAttribute;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.Admin.ProductGroup;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ProductGroupQueryRepository(AppDbContext dbContext) : IProductGroupQueryRepository
{
    public async Task<PagedResult<ProductGroupListItemDTO>> GetListItemsAsync(ProductGroupFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductGroups.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrWhiteSpace(filter.Slug))
            query = query.Where(x => x.Slug.Contains(filter.Slug));
        if (!string.IsNullOrWhiteSpace(filter.Display))
            query = query.Where(x => x.Display.Contains(filter.Display));
        if (!string.IsNullOrWhiteSpace(filter.Breadcrumb))
            query = query.Where(x => x.Breadcrumb.Contains(filter.Breadcrumb));
        if (!string.IsNullOrWhiteSpace(filter.AnchorText))
            query = query.Where(x => x.AnchorText.Contains(filter.AnchorText));
        if (!string.IsNullOrWhiteSpace(filter.AnchorTitle))
            query = query.Where(x => x.AnchorTitle!.Contains(filter.AnchorTitle));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var productGroups = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductGroupListItemDTO>()
        {
            Data = productGroups,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await dbContext.ProductGroups.Select(x => (int?)x.SortOrder).MaxAsync(cancellationToken) ?? 0;

    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductGroups.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ProductGroups.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<ProductGroupDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ProductGroups
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken)
        => await dbContext.ProductGroups.OrderBy(x => x.Name)
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ProductGroups.AnyAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ProductGroupProductAttributeListItemDTO?>> GetAttributesByProductGroupIdAsync(
        int productGroupId,
        CancellationToken cancellationToken)
        => await dbContext.ProductGroupProductAttributes
            .Where(x => x.ProductGroupId == productGroupId)
            .ProjectToDTO()
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ProductGroupProductAttributeDTO>> GetAttributesAsync(int productGroupId,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductGroupProductAttributes
            .Where(x => x.ProductGroupId == productGroupId)
            .OrderBy(x => x.ProductAttributeSortOrder)
                .ThenBy(x => x.ProductAttributeValueSortOrder)
            .Select(ProductGroupProductAttributeExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }
}
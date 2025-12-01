using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ProductAttributeQueryRepository : IProductAttributeQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ProductAttributeQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<ProductAttributeListItemDTO>> GetListItemsAsync(ProductAttributeFilter filter,
        int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductAttributes.AsQueryable();

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
            .Select(x => new ProductAttributeListItemDTO
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Display = x.Display,
                Breadcrumb = x.Breadcrumb,
                DataType = x.DataType.ToString(),
                SortOrder = x.SortOrder,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
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
        var query = _dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug, cancellationToken);
    }

    public async Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken)
        => await _dbContext.ProductAttributes.Select(x => (int?)x.SortOrder)
            .MaxAsync(cancellationToken) ?? 0;

    public async Task<ProductAttributeDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.ProductAttributes
            .Include(x => x.Values)
            .ThenInclude(productAttributeValue => productAttributeValue.CreatedByUser)
            .FirstOrDefaultAsync(pa => pa.Id == id, cancellationToken);

        if (entity == null) return null;

        return new ProductAttributeDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Slug = entity.Slug,
            Display = entity.Display,
            Breadcrumb = entity.Breadcrumb,
            SortOrder = entity.SortOrder,
            DataType = entity.DataType,
            Values = entity.Values.Select(MapToValueDTO)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Value)
                .ToList()
        };
    }

    private static ProductAttributeValueDTO MapToValueDTO(ProductAttributeValue value)
    {
        return value switch
        {
            ProductAttributeBooleanValue bv => new ProductAttributeBooleanValueDTO(
                bv.Id, bv.Value, bv.Slug, bv.Display, bv.Breadcrumb, bv.SortOrder,
                bv.CreatedByUser!.FullName, bv.CreatedAt, bv.BooleanValue),

            ProductAttributeColourValue cv => new ProductAttributeColourValueDTO(
                cv.Id, cv.Value, cv.Slug, cv.Display, cv.Breadcrumb, cv.SortOrder,
                cv.CreatedByUser!.FullName, cv.CreatedAt, cv.HexCode,
                cv.ColourFamily, cv.ColourFamilyHexCode),

            ProductAttributeDecimalValue dv => new ProductAttributeDecimalValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DecimalValue),

            ProductAttributeDateOnlyValue dv => new ProductAttributeDateOnlyValueDTO(
                dv.Id, dv.Value, dv.Slug, dv.Display, dv.Breadcrumb, dv.SortOrder,
                dv.CreatedByUser!.FullName, dv.CreatedAt, dv.DateOnlyValue),

            not null => new ProductAttributeValueDTO(value.Id, value.Value, value.Slug, value.Display, value.Breadcrumb,
                value.SortOrder, value.CreatedByUser!.FullName, value.CreatedAt),

            _ => throw new NotSupportedException(
                $"Product attribute value type '{value.GetType().Name}' is not supported.")
        };
    }

    public async Task<bool> ExistsByValueNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(pa => pa.Values.Any(v => v.Value == name), cancellationToken);
    }

    public async Task<bool> ExistsByValueSlugAsync(string slug, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ProductAttributes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(pa => pa.Values.Any(v => v.Slug == slug), cancellationToken);
    }

    public async Task<IReadOnlyList<ProductAttributeValueDTO?>> GetValuesByProductAttributeIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var productAttributeValues = await _dbContext.ProductAttributeValues
            .Where(x => x.ProductAttributeId == id)
            .OrderBy(x => x.SortOrder).ThenBy(x => x.Value)
            .Include(x => x.CreatedByUser)
            .ToListAsync(cancellationToken);

        return productAttributeValues.Select(MapToValueDTO).ToList();
    }

    public async Task<ProductAttributeValueDTO?> GetValueByProductAttributeValueIdAsync(int id,
        CancellationToken cancellationToken)
    {
        var productAttributeValue = await _dbContext.ProductAttributeValues.Include(x => x.CreatedByUser)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return MapToValueDTO(productAttributeValue);
    }
}
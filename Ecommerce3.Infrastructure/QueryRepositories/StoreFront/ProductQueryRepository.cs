using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
using Ecommerce3.Contracts.DTO.StoreFront.UOM;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.StoreFront;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal class ProductQueryRepository(AppDbContext dbContext) : IProductQueryRepository
{
    private IQueryable<Product> Products => dbContext.Products
        .Where(x => x.Status == ProductStatus.Active)
        .AsQueryable();

    public async Task<IReadOnlyList<ProductListItemDTO>> GetListItemsAsync(string[] sku,
        CancellationToken cancellationToken)
    {
        return await Products
            .Where(x => ((IEnumerable<string>)sku).Contains(x.SKU))
            .AsSplitQuery()
            .Select(ProductExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<ProductListItemDTO>> GetListItemsAsync(int[] categories, int[] brands,
        decimal? minPrice, decimal? maxPrice, List<KeyValuePair<int, decimal>> weights, IDictionary<int, int> attributes,
        SortOrder sortOrder, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = Products;

        //Filter by categories.
        if (categories.Length > 0)  
        {
            var categoryFacets = categories.Select(c => $"category:{c}").ToArray();
            query = query.Where(p => p.Facets.Any(f => ((IEnumerable<string>)categoryFacets).Contains(f)));
        }

        //Filter by brands.
        if (brands.Length > 0) query = query.Where(x => ((IEnumerable<int>)brands).Contains(x.BrandId));

        //Filter by price.
        if (minPrice.HasValue) query = query.Where(p => p.Price >= minPrice.Value);
        if (maxPrice.HasValue) query = query.Where(p => p.Price <= maxPrice.Value);

        //Filter by weights.
        if (weights.Count > 0)
        {
            // Build expected facet strings
            var weightFacets = weights
                .Select(w => $"quantity:{w.Key}:{w.Value}")
                .ToArray();

            query = query.Where(p =>
                p.Facets.Any(f => ((IEnumerable<string>)weightFacets).Contains(f))
            );
        }

        //Filter by attributes.
        foreach (var (attributeId, attributeValueId) in attributes)
        {
            var facet = $"attr:{attributeId}:{attributeValueId}";
            query = query.Where(p => p.Facets.Contains(facet));
        }

        //Filter by sort order.
        query = sortOrder switch
        {
            SortOrder.NameAsc => query.OrderBy(x => x.Name),
            SortOrder.NameDesc => query.OrderByDescending(x => x.Name),
            SortOrder.PriceAsc => query.OrderBy(x => x.Price),
            SortOrder.PriceDesc => query.OrderByDescending(x => x.Price),
            _ => query.OrderByDescending(x => x.CreatedAt)
        };

        // Total count (before pagination)
        var totalProducts = await query.CountAsync(cancellationToken);

        // Pagination
        var skip = (pageNumber - 1) * pageSize;

        var products = await query
            .Skip(skip)
            .Take(pageSize)
            .Select(ProductExpressions.DTOExpression)
            .ToListAsync(cancellationToken);

        return new PagedResult<ProductListItemDTO>
        {
            TotalItems = totalProducts,
            PageNumber = pageNumber,
            PageSize = pageSize,
            Data = products
        };
    }

    public async Task<PriceRangeDTO> GetPriceRangeAsync(int[] categories, CancellationToken cancellationToken)
    {
        return await dbContext.ProductCategories
                   .Where(p =>
                       ((IEnumerable<int>)categories).Contains(p.CategoryId) &&
                       p.Category!.IsActive &&
                       p.Product!.Status == ProductStatus.Active)
                   .GroupBy(_ => 1)
                   .Select(g => new PriceRangeDTO
                   {
                       MinPrice = g.Min(x => x.Product!.Price),
                       MaxPrice = g.Max(x => x.Product!.Price)
                   })
                   .FirstOrDefaultAsync(cancellationToken)
               ?? new PriceRangeDTO
               {
                   MinPrice = 0,
                   MaxPrice = 0
               };
    }

    public async Task<IReadOnlyList<UOMFacetDTO>> GetWeightsAsync(int[] categories, CancellationToken cancellationToken)
    {
        return await dbContext.ProductCategories
            .Where(p =>
                ((IEnumerable<int>)categories).Contains(p.CategoryId) &&
                p.Category!.IsActive &&
                p.Product!.Status == ProductStatus.Active &&
                p.Product.UnitOfMeasure!.Type == UnitOfMeasureType.Weight)
            .GroupBy(p => new
            {
                p.Product!.UnitOfMeasureId,
                p.Product.UnitOfMeasure!.SingularName,
                p.Product.UnitOfMeasure.PluralName,
                p.Product.QuantityPerUnitOfMeasure,
                p.Product.UnitOfMeasure.DecimalPlaces
            })
            .Select(g => new UOMFacetDTO
            {
                Id = g.Key.UnitOfMeasureId,
                SingularName = g.Key.SingularName,
                PluralName = g.Key.PluralName,
                QtyPerUOM = g.Key.QuantityPerUnitOfMeasure,
                DecimalPlaces = g.Key.DecimalPlaces
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductAttributeFacetDTO>> GetAttributesAsync(int[] categories,
        CancellationToken cancellationToken)
    {
        return await dbContext.ProductCategories
            .Where(pc =>
                ((IEnumerable<int>)categories).Contains(pc.CategoryId) &&
                pc.Category!.IsActive &&
                pc.Product!.Status == ProductStatus.Active)
            .SelectMany(pc => pc.Product!.Attributes)
            .GroupBy(a => new
            {
                a.ProductAttributeId,
                AttributeDisplay = a.ProductAttribute!.Display,
                a.ProductAttributeValueId,
                AttributeValueDisplay = a.ProductAttributeValue!.Display,
                a.ProductAttributeSortOrder,
                a.ProductAttributeValueSortOrder
            })
            .OrderBy(g => g.Key.ProductAttributeSortOrder)
            .ThenBy(g => g.Key.ProductAttributeValueSortOrder)
            .Select(g => new ProductAttributeFacetDTO
            {
                AttributeId = g.Key.ProductAttributeId,
                AttributeDisplay = g.Key.AttributeDisplay,
                AttributeValueId = g.Key.ProductAttributeValueId,
                AttributeValueDisplay = g.Key.AttributeValueDisplay
            })
            .ToListAsync(cancellationToken);
    }
}
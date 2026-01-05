using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.StoreFront.Product;
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

    public async Task<IReadOnlyList<ProductListItemDTO>> GetListAsync(string[] sku,
        CancellationToken cancellationToken)
    {
        return await Products
            .Where(x => ((IEnumerable<string>)sku).Contains(x.SKU))
            .AsSplitQuery()
            .Select(ProductExpressions.DTOExpression)
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<ProductListItemDTO>> GetListAsync(int[] categories, int[] brands, decimal? minPrice,
        decimal? maxPrice, IDictionary<int, decimal> weights, IDictionary<int, int> attributes,
        SortOrder sortOrder, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = Products;

        //Filter by categories.
        if (categories.Length > 0)
        {
            var categoryFacets = categories.Select(c => $"cat:{c}").ToArray();
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
            query = query.Where(p =>
                weights.Any(w =>
                    p.UnitOfMeasureId == w.Key &&
                    p.QuantityPerUnitOfMeasure == w.Value
                ));
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
}
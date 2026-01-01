using Ecommerce3.Contracts.DTO.Admin.ProductGroup;
using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.Admin;

internal sealed class ProductGroupProductAttributeQueryRepository(
    AppDbContext dbContext,
    IDbConnectionFactory dbConnectionFactory) : IProductGroupProductAttributeQueryRepository
{
    public async Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken)
    {
        var maxSortOrder = await dbContext.ProductGroupProductAttributes
            .Where(x => x.ProductGroupId == productGroupId)
            .Select(x => (decimal?)x.ProductAttributeSortOrder)
            .MaxAsync(cancellationToken);

        return (maxSortOrder ?? 0m) + 1m;
    }
    
    public async Task<ProductAttributeEditDTO> GetByParamsAsync(int productGroupId, int productAttributeId, 
        CancellationToken cancellationToken)
    {
        // Query 1: Get ProductAttribute data and sort order
        var attributeData = await (from pgpa in dbContext.ProductGroupProductAttributes
            join pa in dbContext.ProductAttributes on pgpa.ProductAttributeId equals pa.Id
            where pgpa.ProductGroupId == productGroupId && pgpa.ProductAttributeId == productAttributeId
            select new 
            {
                ProductAttributeId = pa.Id,
                pa.Name,
                pgpa.ProductAttributeSortOrder
            })
            .FirstOrDefaultAsync(cancellationToken);
        

        // Query 2: Get all attribute values with left join to selected values
        var allValues = await (from pav in dbContext.ProductAttributeValues
            join pgpa in dbContext.ProductGroupProductAttributes on new { pav.Id, pav.ProductAttributeId } 
                equals new { Id = pgpa.ProductAttributeValueId, pgpa.ProductAttributeId } into pgpaGroup
            from pgpa in pgpaGroup.Where(p => p.ProductGroupId == productGroupId).DefaultIfEmpty()
            where pav.ProductAttributeId == productAttributeId
            select new ProductAttributeValueEditDTO
            {
                ProductAttributeValueId = pav.Id,
                Value = pav.Value,
                Display = pav.Display,
                ProductAttributeValueSortOrder = pgpa != null ? pgpa.ProductAttributeValueSortOrder : (decimal?)null,
                IsSelected = pgpa != null
            })
            .OrderBy(v => v.IsSelected ? 0 : 1)
            .ThenBy(v => v.ProductAttributeValueSortOrder ?? decimal.MaxValue)
            .ToListAsync(cancellationToken);

        // Merge: Combine the results
        return new ProductAttributeEditDTO
        {
            ProductAttributeId = attributeData.ProductAttributeId,
            Name = attributeData.Name,
            ProductAttributeSortOrder = attributeData.ProductAttributeSortOrder,
            Values = allValues
        };
    }
}
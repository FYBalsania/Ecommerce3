using Ecommerce3.Contracts.DTO.API.ProductGroup;
using Ecommerce3.Contracts.QueryRepositories.API;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.API;

internal sealed class ProductGroupProductAttributeQueryRepository(AppDbContext dbContext)
    : IProductGroupProductAttributeQueryRepository
{
    public async Task<ProductGroupProductAttributeViewDTO?> GetAsync(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken)
    {
        var query = from a in dbContext.ProductGroupProductAttributes
            join b in dbContext.ProductAttributes on a.ProductAttributeId equals b.Id
            where a.ProductGroupId == productGroupId && a.ProductAttributeId == productAttributeId
            select new ProductGroupProductAttributeViewDTO
            {
                Id = a.ProductAttributeId,
                Name = b.Name,
                SortOrder = a.ProductAttributeSortOrder,
                Values = b.Values.Select(x => new ProductGroupProductAttributeValueViewDTO
                {
                    Id = x.Id,
                    Value = x.Value,
                    Display = x.Display,
                    IsSelected = false,
                    SortOrder = 1
                }).ToList()
            };

        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
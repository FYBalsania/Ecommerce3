using Dapper;
using Ecommerce3.Contracts.DTO.Admin.ProductGroup;
using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.Admin;

internal sealed class ProductGroupProductAttributeQueryRepository(
    AppDbContext dbContext,
    IDbConnectionFactory dbConnectionFactory)
    : IProductGroupProductAttributeQueryRepository
{
    public async Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken)
    {
        return await dbContext.ProductGroupProductAttributes
            .Where(x => x.ProductGroupId == productGroupId)
            .Select(x => (decimal?)x.ProductAttributeSortOrder)
            .MaxAsync(cancellationToken) ?? 0m;
    }

    public async Task<ProductAttributeEditDTO?> GetByParamsAsync(int productGroupId, int productAttributeId,
        CancellationToken cancellationToken)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        var sql = """
                  SELECT 
                      b."Id" AS "ProductAttributeId",
                      b."Name",
                      c."ProductAttributeSortOrder",
                      a."Id" AS "ProductAttributeValueId",
                      a."Value",
                      a."Display",
                      c."ProductAttributeValueSortOrder"
                  FROM "ProductAttributeValue" a
                           INNER JOIN "ProductAttribute" b ON a."ProductAttributeId" = b."Id"
                           LEFT JOIN "ProductGroupProductAttribute" c 
                               on a."Id" = c."ProductAttributeValueId" AND c."ProductGroupId"=@ProductGroupId
                  WHERE a."ProductAttributeId"=@ProductAttributeId
                  """;

        ProductAttributeEditDTO? paDTO = null;
        var foo = await connection
            .QueryAsync<ProductAttributeEditDTO, ProductAttributeValueEditDTO, ProductAttributeEditDTO>(
                sql,
                (pa, pav) =>
                {
                    paDTO ??= pa;
                    paDTO.Values.Add(pav);
                    return pa;
                },
                new { ProductGroupId = productGroupId, ProductAttributeId = productAttributeId },
                splitOn: "ProductAttributeValueId");

        return paDTO;
    }
}
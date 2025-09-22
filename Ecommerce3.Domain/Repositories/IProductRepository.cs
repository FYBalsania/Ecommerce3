using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    public Task<(IEnumerable<ProductListItem> ListItems, int Count)?> GetProductListItemsAsync(string? skuCode,
        string? gtin, string? mpn, string? mfc, string? ean, string? upc, string? name, string? slug, int? brandId,
        int? productGroupId, int? deliveryWindowId, ProductStatus productStatus, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    public Task<bool> ExistsBySkuCodeAsync(string skuCode, CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
    public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken);
    public Task<Product?> GetBySkuCodeAsync(string skuCode, CancellationToken cancellationToken);
}
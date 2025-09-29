using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    public Task<(IEnumerable<Product> ListItems, int Count)?> GetProductsAsync(string? skuCode,
        string? gtin, string? mpn, string? mfc, string? ean, string? upc, string? name, string? slug, int? brandId,
        int? categoryId, int? productGroupId, int? deliveryWindowId, ProductStatus productStatus,
        ProductInclude[] includes, bool trackChanges,
        int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsBySKUCodeAsync(string skuCode, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);

    public Task<Product?> GetBySlugAsync(string slug, ProductInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Product?> GetByNameAsync(string name, ProductInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Product?> GetBySKUCodeAsync(string skuCode, ProductInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Product?> GetByIdAsync(int id, ProductInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);
}
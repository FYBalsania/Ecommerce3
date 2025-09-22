using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Product> ListItems, int Count)?> GetProductsAsync(string? skuCode,
        string? gtin, string? mpn, string? mfc, string? ean, string? upc, string? name, string? slug, int? brandId,
        int? productGroupId, int? deliveryWindowId, ProductStatus productStatus, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsBySkuCodeAsync(string skuCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product?> GetBySkuCodeAsync(string skuCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IEnumerable<Product> ListItems, int Count)?> GetProductsAsync(string? skuCode, string? gtin,
        string? mpn, string? mfc, string? ean, string? upc, string? name, string? slug, int? brandId, int? categoryId,
        int? productGroupId, int? deliveryWindowId, ProductStatus productStatus, ProductInclude includes,
        bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetBySlugAsync(string slug, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByNameAsync(string name, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetBySKUCodeAsync(string skuCode, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByIdAsync(int id, ProductInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
namespace Ecommerce3.Domain.Stubs;

public interface IRepository<T> where T : Entity
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
}

public interface IImageRepository<T> : IRepository<T> where T : Image
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
}

public interface IBrandImageRepository : IImageRepository<BrandImage>
{
    Task<BrandImage?> GetByBrandIdAsync(int brandId, CancellationToken cancellationToken);
}

public interface ICategoryImageRepository : IImageRepository<CategoryImage>
{
    Task<CategoryImage?> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
}

public interface IProductImageRepository : IImageRepository<ProductImage>
{
    Task<ProductImage?> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
}

public interface IBrandRepository : IRepository<Brand>
{
    Task<Brand?> GetByNameAsync(string name, CancellationToken cancellationToken);
}

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken);
}

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken);
}
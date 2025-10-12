namespace Ecommerce3.Domain.Stubs;

public interface IImageService<T> where T : Image
{
    Task AddAsync(T entity, CancellationToken cancellationToken);
}

public interface IBrandImageService : IImageService<BrandImage>
{
}
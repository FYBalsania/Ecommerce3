using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IImageRepository<T> : IRepository<T> where T : Image
{
    public Type ImageType { get; }
    
    public Task<(IEnumerable<T> ListItems, int Count)?> GetImagesAsync(string? fileName,
        int? imageTypeId, ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId,
        int? productId, int? pageId, int? productGroupId, ImageInclude includes, bool trackChanges,
        int pageNumber, int pageSize, CancellationToken cancellationToken);
    
    public Task<Image?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken);
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IImageRepository : IRepository<Image>
{
    public Task<(IEnumerable<Image> ListItems, int Count)?> GetImagesAsync(string? fileName,
        int? imageTypeId, ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId,
        int? productId, int? pageId, int? productGroupId, ImageInclude[] includes, bool trackChanges,
        int pageNumber, int pageSize, CancellationToken cancellationToken);
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IImageRepository : IRepository<Image>
{
    public Task<(IEnumerable<ImageListItem> ListItems, int Count)?> GetImageListItemsAsync(string? fileName,
        int? imageTypeId, ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId,
        int? productId, int? pageId, int? productGroupId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}
using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ImageRepository<T> : Repository<T>, IImageRepository<T> where T : Image
{
    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IEnumerable<T> ListItems, int Count)?> GetImagesAsync(string? fileName, int? imageTypeId,
        ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId, int? productId, int? pageId,
        int? productGroupId, ImageInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
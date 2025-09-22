using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ImageRepository : Repository<Image>, IImageRepository
{
    private readonly AppDbContext _dbContext;

    public ImageRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Image> ListItems, int Count)?> GetImagesAsync(string? fileName,
        int? imageTypeId, ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId,
        int? productId, int? pageId, int? productGroupId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
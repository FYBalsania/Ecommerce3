using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ImageRepository<T> : Repository<T>, IImageRepository<T> where T : Image
{
    private readonly AppDbContext _dbContext;

    public ImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Type ImageType  => typeof(T);

    public async Task<(IEnumerable<T> ListItems, int Count)?> GetImagesAsync(string? fileName, int? imageTypeId,
        ImageSize? imageSize, string? title, string? link, int? brandId, int? categoryId, int? productId, int? pageId,
        int? productGroupId, ImageInclude includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Image?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges
            ? _dbContext.Images.AsTracking()
            : _dbContext.Images.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
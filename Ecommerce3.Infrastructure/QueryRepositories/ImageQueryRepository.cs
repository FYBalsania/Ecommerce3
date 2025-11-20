using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal abstract class ImageQueryRepository : IImageQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ImageQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public abstract Type ImageType { get; }
    public abstract Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken);
    
    public async Task<ImageDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Images.Where(x => x.Id == id)
            .Select(x => new ImageDTO
            {
                Id = x.Id,
                OgFileName = x.OgFileName,
                FileName = x.FileName,
                FileExtension = x.FileExtension,
                ImageTypeId = x.ImageTypeId,
                ImageTypeName = x.ImageType!.Name,
                ImageTypeSlug = x.ImageType!.Slug,
                Size = x.Size,
                AltText = x.AltText,
                Title = x.Title,
                Loading = x.Loading,
                Link = x.Link,
                LinkTarget = x.LinkTarget,
                SortOrder = x.SortOrder,
                CreatedAppUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt,
                UpdatedAppUserFullName = x.UpdatedByUser!.FullName,
                UpdatedAt = x.UpdatedAt
            }).FirstAsync(cancellationToken);
    }
}
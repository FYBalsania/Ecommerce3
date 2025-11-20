using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class BrandImageQueryRepository : ImageQueryRepository
{
    private readonly AppDbContext _dbContext;

    public BrandImageQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override Type ImageType => typeof(BrandImage);

    public override async Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken)
    {
        return await _dbContext.BrandImages.Where(x => x.BrandId == parentId)
            .OrderBy(x => x.ImageType!.Name).ThenBy(x => x.Size).ThenBy(x => x.SortOrder)
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
            })
            .ToListAsync(cancellationToken);
    }
}
using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class CategoryImageQueryRepository : IImageQueryRepository
{
    private readonly AppDbContext _dbContext;

    public CategoryImageQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Type ImageType => typeof(CategoryImage);
    
    public async Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken)
    {
        return await _dbContext.CategoryImages.Where(x => x.CategoryId == parentId)
            .OrderBy(x => x.ImageType!.Name).ThenBy(x => x.Size).ThenBy(x => x.SortOrder)
            .Select(x => new ImageDTO
            {
                Id = x.Id,
                OgFileName = x.OgFileName,
                FileName = x.FileName,
                FileExtension = x.FileExtension,
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
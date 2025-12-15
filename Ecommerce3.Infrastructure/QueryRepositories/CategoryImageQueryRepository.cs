using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class CategoryImageQueryRepository(AppDbContext dbContext) : ImageQueryRepository(dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public override Type ImageType => typeof(CategoryImage);
    
    public override async Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken)
        => await _dbContext.CategoryImages
            .Where(x => x.CategoryId == parentId)
            .OrderBy(x => x.ImageType!.Name)
            .ThenBy(x => x.Size)
            .ThenBy(x => x.SortOrder)
            .ProjectToDTO()
            .ToListAsync(cancellationToken);
}
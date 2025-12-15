using Ecommerce3.Contracts.DTOs.Image;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal abstract class ImageQueryRepository(AppDbContext dbContext) : IImageQueryRepository
{
    public abstract Type ImageType { get; }
    public abstract Task<IReadOnlyList<ImageDTO>> GetByParentIdAsync(int parentId, CancellationToken cancellationToken);
    
    public async Task<ImageDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Images
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstAsync(cancellationToken);
}
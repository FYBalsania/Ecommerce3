using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class ImageTypeQueryRepository(AppDbContext dbContext) : IImageTypeQueryRepository
{
    public async Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.ImageTypes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Entity))
            query = query.Where(x => x.Entity!.Contains(filter.Entity));
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var imageType = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
            .ToListAsync(cancellationToken);

        return new PagedResult<ImageTypeListItemDTO>()
        {
            Data = imageType,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }

    public async Task<bool> ExistsByNameForEntityAsync(string name, string entity, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ImageTypes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name && x.Entity == entity, cancellationToken);
    }
    
    public async Task<bool> ExistsBySlugForEntityAsync(string slug, string entity, int? excludeId, CancellationToken cancellationToken)
    {
        var query = dbContext.ImageTypes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Slug == slug, cancellationToken);

        return await query.AnyAsync(x => x.Slug == slug && x.Entity == entity, cancellationToken);
    }

    public async Task<ImageTypeDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ImageTypes
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstAsync(cancellationToken);

    public async Task<Dictionary<int, string>> GetIdAndNamesByEntityAsync(string entity, CancellationToken cancellationToken)
        => await dbContext.ImageTypes.OrderBy(x => x.Name)
            .Where(x => x.Entity == entity)
            .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.ImageTypes.AnyAsync(x => x.Id == id, cancellationToken);
}
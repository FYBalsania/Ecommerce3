using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class ImageTypeQueryRepository : IImageTypeQueryRepository
{
    private readonly AppDbContext _dbContext;

    public ImageTypeQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.ImageTypes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Entity))
            query = query.Where(x => x.Entity.Contains(filter.Entity));
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Name);
        var imageType = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ImageTypeListItemDTO
            {
                Id = x.Id,
                Entity = x.Entity,
                Name = x.Name,
                IsActive = x.IsActive,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<ImageTypeListItemDTO>()
        {
            Data = imageType,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }
    
    public async Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.ImageTypes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Name == name, cancellationToken);

        return await query.AnyAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<ImageTypeDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.ImageTypes
            .Where(x => x.Id == id)
            .Select(x => new ImageTypeDTO
            {
                Id = x.Id,
                Entity = x.Entity,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
            }).FirstAsync(cancellationToken);
    }
}
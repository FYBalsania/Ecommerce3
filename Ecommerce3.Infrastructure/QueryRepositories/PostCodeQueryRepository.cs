using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class PostCodeQueryRepository  : IPostCodeQueryRepository
{
    private readonly AppDbContext _dbContext;

    public PostCodeQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.PostCodes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Code))
            query = query.Where(x => x.Code.Contains(filter.Code));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Code);
        var postCodes = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new PostCodeListItemDTO
            {
                Id = x.Id,
                Code = x.Code,
                IsActive = x.IsActive,
                CreatedUserFullName = x.CreatedByUser!.FullName,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<PostCodeListItemDTO>()
        {
            Data = postCodes,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = total
        };
    }
    
    public async Task<bool> ExistsByCodeAsync(string name, int? excludeId, CancellationToken cancellationToken)
    {
        var query = _dbContext.PostCodes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Code == name, cancellationToken);

        return await query.AnyAsync(x => x.Code == name, cancellationToken);
    }

    public async Task<PostCodeDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.PostCodes
            .Where(x => x.Id == id)
            .Select(x => new PostCodeDTO
            {
                Id = x.Id,
                Code = x.Code,
                IsActive = x.IsActive,
            }).FirstAsync(cancellationToken);
    }
}
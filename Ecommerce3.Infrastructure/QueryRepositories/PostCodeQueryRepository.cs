using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.Admin;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal sealed class PostCodeQueryRepository(AppDbContext dbContext) : IPostCodeQueryRepository
{
    public async Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber,
        int pageSize, CancellationToken cancellationToken)
    {
        var query = dbContext.PostCodes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Code))
            query = query.Where(x => x.Code.Contains(filter.Code));
        if (filter.IsActive.HasValue)
            query = query.Where(x => x.IsActive == filter.IsActive);

        var total = await query.CountAsync(cancellationToken);
        query = query.OrderBy(x => x.Code);
        var postCodes = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ProjectToListItemDTO()
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
        var query = dbContext.PostCodes.AsQueryable();

        if (excludeId is not null)
            return await query.AnyAsync(x => x.Id != excludeId && x.Code == name, cancellationToken);

        return await query.AnyAsync(x => x.Code == name, cancellationToken);
    }

    public async Task<PostCodeDTO> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.PostCodes
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstAsync(cancellationToken);
}
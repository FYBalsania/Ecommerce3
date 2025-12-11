using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Extensions.StoreFront;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal sealed class PageQueryRepository(AppDbContext dbContext) : IPageQueryRepository
{
    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        return await dbContext.Pages
            .Where(x => EF.Functions.ILike(x.Path!, path) && x.IsActive)
            // .AsSplitQuery()
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
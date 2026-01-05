using Ecommerce3.Contracts.DTO.StoreFront.Page;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Expressions.StoreFront;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories.StoreFront;

internal sealed class PageQueryRepository(AppDbContext dbContext) : IPageQueryRepository
{
    public async Task<PageDTO?> GetByPathAsync(string path, CancellationToken cancellationToken)
    {
        return await dbContext.Pages
            .Where(x => EF.Functions.ILike(x.Path!, path) && x.IsActive)
            .AsSplitQuery()
            .Select(PageExpressions.DTOExpression)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PageDTO?> GetByCategoryIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.CategoryPages
            .Where(x => x.CategoryId == id)
            .AsSplitQuery()
            .Select(PageExpressions.DTOExpression)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
using Ecommerce3.Application.Mappers;
using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class TextListItemQueryRepository : ITextListItemQueryRepository
{
    private readonly AppDbContext _dbContext;

    public TextListItemQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.TextListItems
            .Where(x => x.Id == id)
            .ProjectToDTO()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
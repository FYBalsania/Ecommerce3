using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class TextListItemQueryRepository : ITextListItemQueryRepository
{
    private readonly AppDbContext _dbContext;

    protected TextListItemQueryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
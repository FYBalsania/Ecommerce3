using Ecommerce3.Contracts.DTOs.TextListItem;
using Ecommerce3.Contracts.QueryRepositories;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Data;
using Ecommerce3.Infrastructure.Repositories;

namespace Ecommerce3.Infrastructure.QueryRepositories;

internal class TextListItemQueryRepository : Repository<TextListItem>, ITextListItemQueryRepository
{
    private readonly AppDbContext _dbContext;

    protected TextListItemQueryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TextListItemDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
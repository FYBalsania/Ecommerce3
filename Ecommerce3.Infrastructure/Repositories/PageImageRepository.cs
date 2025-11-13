using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class PageImageRepository : ImageRepository<PageImage>, IPageImageRepository
{
    private readonly AppDbContext _dbContext;

    public PageImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PageImage?> GetByPageIdAsync(int pageId, PageImageInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
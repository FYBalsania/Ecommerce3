using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ImageTypeRepository : Repository<ImageType>, IImageTypeRepository
{
    private readonly AppDbContext _dbContext;

    public ImageTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    private IQueryable<ImageType> GetQuery(bool trackChanges)
        => trackChanges
            ? _dbContext.ImageTypes.AsQueryable()
            : _dbContext.ImageTypes.AsNoTracking();

    public async Task<ImageType?> GetByIdAsync(int id, bool trackChanges, CancellationToken cancellationToken) 
        => await GetQuery(trackChanges).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}
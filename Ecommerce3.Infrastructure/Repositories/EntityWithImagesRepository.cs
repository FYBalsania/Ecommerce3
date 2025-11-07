using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Repositories;

internal abstract class EntityWithImagesRepository<TEntity, TImage> : Repository<TEntity>,
    IEntityWithImagesRepository<TEntity, TImage>
    where TEntity : EntityWithImages<TImage>
    where TImage : Image
{
    private readonly AppDbContext _dbContext;

    protected EntityWithImagesRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public Type EntityType => typeof(TEntity);
    public Type ImageType => typeof(TImage);

    public async Task<Entity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
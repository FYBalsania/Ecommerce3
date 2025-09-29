using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class ImageTypeRepository : Repository<ImageType>, IImageTypeRepository
{
    private readonly AppDbContext _dbContext;

    public ImageTypeRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;
}
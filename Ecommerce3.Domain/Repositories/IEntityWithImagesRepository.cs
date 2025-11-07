using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IEntityWithImagesRepository<TEntity, TImage> : IRepository<TEntity>, IImageEntityRepository
    where TEntity : EntityWithImages<TImage>
    where TImage : Image
{
}
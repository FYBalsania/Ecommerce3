using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeValueRepository<T> : IRepository<T> where T : ProductAttributeValue
{
    Task<T?> GetByIdAsync(int id, ProductAttributeValueInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}
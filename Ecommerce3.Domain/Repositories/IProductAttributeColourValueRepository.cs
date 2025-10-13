using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductAttributeColourValueRepository : IProductAttributeValueRepository<ProductAttributeColourValue>
{
    public Task<(IReadOnlyCollection<ProductAttributeColourValue> ListItems, int Count)>
        GetProductAttributeColourValuesAsync(string? name, ProductAttributeColourValueInclude includes,
            bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
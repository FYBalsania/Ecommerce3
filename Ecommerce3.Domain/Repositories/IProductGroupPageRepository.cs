using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IProductGroupPageRepository : IPageRepository<ProductGroupPage>
{
    Task<ProductGroupPage?> GetByProductGroupIdAsync(int productGroupId, ProductGroupPageInclude include,
        bool trackChanges, CancellationToken cancellationToken);
}
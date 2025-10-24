using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductGroupPageQueryRepository : IPageRepository<ProductGroupPage>
{
    Task<ProductGroupPage?> GetByProductGroupIdAsync(int productGroupId, ProductGroupPageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}
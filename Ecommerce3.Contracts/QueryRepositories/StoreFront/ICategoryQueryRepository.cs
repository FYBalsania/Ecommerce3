using Ecommerce3.Contracts.DTO.StoreFront.Category;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface ICategoryQueryRepository
{
    Task<IReadOnlyList<CategoryListItemDTO>> GetListAsync(CancellationToken cancellationToken);
}
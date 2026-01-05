using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;

namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface ICategoryQueryRepository
{
    Task<IReadOnlyList<CategoryListItemDTO>> GetListAsync(CancellationToken cancellationToken);
    Task<PLPParentCategoryDTO?> GetWithChildrenBySlugAsync(string slug, CancellationToken cancellationToken);
    Task<int[]> GetDescendantIdsAsync(int categoryId, CancellationToken cancellationToken);
}
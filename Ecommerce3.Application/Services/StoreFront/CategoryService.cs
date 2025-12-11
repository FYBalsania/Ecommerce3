using Ecommerce3.Application.Services.StoreFront.Interfaces;
using Ecommerce3.Contracts.DTO.StoreFront.Category;
using Ecommerce3.Contracts.QueryRepositories.StoreFront;

namespace Ecommerce3.Application.Services.StoreFront;

internal sealed class CategoryService(ICategoryQueryRepository categoryQueryRepository) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryListItemDTO>> GetListItemsAsync(CancellationToken cancellationToken)
    {
        return await categoryQueryRepository.GetListAsync(cancellationToken);
    }
}
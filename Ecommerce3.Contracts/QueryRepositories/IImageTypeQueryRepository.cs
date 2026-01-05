using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IImageTypeQueryRepository
{
    Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<bool> ExistsByNameForEntityAsync(string name, string entity, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsBySlugForEntityAsync(string slug, string entity, int? excludeId, CancellationToken cancellationToken);
    Task<ImageTypeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNamesByEntityAsync(string entity,CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}
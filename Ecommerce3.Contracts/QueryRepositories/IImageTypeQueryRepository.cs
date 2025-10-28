using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IImageTypeQueryRepository
{
    Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<ImageTypeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
}
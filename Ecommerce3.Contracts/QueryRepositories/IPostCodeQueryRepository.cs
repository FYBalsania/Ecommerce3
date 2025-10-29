using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IPostCodeQueryRepository
{
    Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    public Task<bool> ExistsByCodeAsync(string code, int? excludeId, CancellationToken cancellationToken);
    public Task<PostCodeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
}
using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IBankQueryRepository
{
    Task<PagedResult<BankListItemDTO>> GetListItemsAsync(BankFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
    public Task<BankDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
}
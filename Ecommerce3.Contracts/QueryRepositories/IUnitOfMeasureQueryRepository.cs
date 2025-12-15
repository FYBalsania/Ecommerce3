using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories;

public interface IUnitOfMeasureQueryRepository
{
    Task<PagedResult<UnitOfMeasureListItemDTO>> GetListItemsAsync(UnitOfMeasureFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<UnitOfMeasureDTO?> GetByUnitOfMeasureIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByCodeAsync(string code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(int? excludeId = null, bool excludeNonBases = false, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}
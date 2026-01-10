using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Contracts.QueryRepositories.Admin;

public interface ICountryQueryRepository
{
    Task<PagedResult<CountryListItemDTO>> GetListItemsAsync(CountryFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByIso2CodeAsync(string iso2Code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByIso3CodeAsync(string iso3Code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByNumericCodeAsync(string numericCode, int? excludeId, CancellationToken cancellationToken);
    public Task<CountryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
}
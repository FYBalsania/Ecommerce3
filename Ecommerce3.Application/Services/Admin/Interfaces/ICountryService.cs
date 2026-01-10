using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Admin.Country;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Admin.Interfaces;

public interface ICountryService
{
    Task<PagedResult<CountryListItemDTO>> GetListItemsAsync(CountryFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddCountryCommand command, CancellationToken cancellationToken);
    Task<CountryDTO?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditCountryCommand command, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
}
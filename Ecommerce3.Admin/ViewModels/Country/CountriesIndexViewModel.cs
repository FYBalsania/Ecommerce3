using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTO.Admin.Country;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Country;

public record CountriesIndexViewModel
{
    public CountryFilter Filter { get; init; }
    public PagedResult<CountryListItemDTO> Countries { get; init; } 
}
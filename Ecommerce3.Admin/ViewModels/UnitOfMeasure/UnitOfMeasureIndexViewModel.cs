using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.UnitOfMeasure;

public record UnitOfMeasureIndexViewModel
{
    public UnitOfMeasureFilter Filter { get; init; }
    public PagedResult<UnitOfMeasureListItemDTO> UnitOfMeasures { get; init; }
    public string PageTitle { get; init; }
}
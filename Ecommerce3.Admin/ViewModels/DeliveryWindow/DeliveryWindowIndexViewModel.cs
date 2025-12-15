using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.DeliveryWindow;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.DeliveryWindow;

public record DeliveryWindowIndexViewModel
{
    public DeliveryWindowFilter Filter { get; init; }
    public PagedResult<DeliveryWindowListItemDTO> DeliveryWindows { get; init; }
    public string PageTitle { get; init; }
}
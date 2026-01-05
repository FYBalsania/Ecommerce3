using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Product;

public record InventoryIndexViewModel
{
    public InventoryFilter Filter { get; init; }
    public PagedResult<InventoryListItemDTO> Inventories { get; init; }
}
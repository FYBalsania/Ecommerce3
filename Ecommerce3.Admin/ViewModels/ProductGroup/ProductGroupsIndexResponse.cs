using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ProductGroup;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.ProductGroup;

public record ProductGroupsIndexResponse
{
    public ProductGroupFilter Filter { get; init; }
    public PagedResult<ProductGroupListItemDTO> ProductGroups { get; init; }
}